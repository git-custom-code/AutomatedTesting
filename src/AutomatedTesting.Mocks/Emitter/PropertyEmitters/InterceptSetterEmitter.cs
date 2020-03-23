namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
    using Extensions;
    using Interception;
    using Interception.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property setters
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
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
    ///     _interceptor.Intercept(incovation);
    ///     return returnValueFeature.ReturnValue;
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
    ///     _interceptor.Intercept(incovation);
    ///     return returnValueFeature.ReturnValue;
    /// ]]>
    /// </remarks>
    public sealed class InterceptSetterEmitter : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptSetterEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptSetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc />
        public override void EmitPropertyImplementation()
        {
            var features = new List<LocalBuilder>();
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
            body.EmitInterceptCall(InterceptorField, invocation);
            body.EmitReturnStatement();

            property.SetSetMethod(setter);
        }

        #endregion
    }
}