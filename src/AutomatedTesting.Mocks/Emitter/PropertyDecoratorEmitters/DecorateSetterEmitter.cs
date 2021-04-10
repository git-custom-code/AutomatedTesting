namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
    using Extensions;
    using Interception;
    using Interception.Parameters;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property setters
    /// that will forward any calls to either an injected <see cref="IInterceptor.Intercept(IInvocation)"/>
    /// instance or to the original implementation of a decoratee instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(set_Property),
    ///         Array.Empty<Type>());
    ///
    ///     var propertySetterValueFeature = new PropertySetterValue(propertySignature, value);
    ///
    ///     var incovation = new Invocation(methodSignature, propertySetterValueFeature);
    ///     if (!_interceptor.Intercept(incovation))
    ///     {
    ///         _decoratee.Property = value;
    ///         return;
    ///     }
    ///
    ///     return;
    /// ]]>
    ///
    /// or
    ///
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(set_Property),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var propertySetterValueFeature = new PropertySetterValue(propertySignature, value);
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///
    ///     var incovation = new Invocation(methodSignature, propertySetterValueFeature, parameterInFeature);
    ///     if (!_interceptor.Intercept(incovation))
    ///     {
    ///         _decoratee.Property = value;
    ///         return;
    ///     }
    ///
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class DecorateSetterEmitter : PropertyDecoratorEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="DecorateSetterEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public DecorateSetterEmitter(
            TypeBuilder type,
            PropertyInfo signature,
            FieldBuilder decorateeField,
            FieldBuilder interceptorField)
            : base(type, signature, decorateeField, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc cref="PropertyDecoratorEmitterBase" />
        public override void EmitPropertyImplementation()
        {
            var
                features = new List<LocalBuilder>();
            var parameters = Signature.GetIndexParameters();
            var types = parameters.Select(p => p.ParameterType).ToArray();
            var typesAndValue = types.Concat(new[] { Signature.PropertyType }).ToArray();
            var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();

            var property = Type.DefineProperty(
                Signature.Name,
                PropertyAttributes.None,
                Signature.PropertyType,
                types);

            var setterSignature = Signature.GetSetMethod() ?? throw new MethodInfoException(Type, $"set_{Signature.Name}");
            var setter = Type.DefineMethod(
                setterSignature.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                null,
                typesAndValue);
            var body = setter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var propertySignature);
            body.EmitLocalMethodSignatureVariable(out var methodSignature);
            body.EmitLocalPropertySetterValueFeatureVariable(out var setterValue);
            features.Add(setterValue);
            if (inParameters.Length > 0)
            {
                features.Add(body.EmitLocalParameterFeatureVariable<ParameterIn>());
            }
            body.EmitLocalInvocationVariable(out var invocation);

            // body
            body.EmitGetPropertySignature(Signature, propertySignature);
            body.EmitGetMethodSignature(setterSignature, methodSignature);
            body.EmitNewPropertySetterValueFeature(Signature, propertySignature, setterValue, typesAndValue.Length);
            if (inParameters.Length > 0)
            {
                body.EmitNewParameterFeature<ParameterIn>(methodSignature, parameters, features[1]);
            }
            body.EmitNewInvocation(invocation, methodSignature, features);
            body.EmitIfInterceptCall(InterceptorField, invocation, out var elseLabel);
            body.EmitDecorateeCall(setterSignature, parameters, DecorateeField, passSetterValue: true);
            body.EmitReturnStatement();
            body.EmitElseInterceptCall(elseLabel);
            body.EmitReturnStatement();

            property.SetSetMethod(setter);
        }

        #endregion
    }
}