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
            var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();
            // ToDo: Ref/Out

            var property = Type.DefineProperty(
                Signature.Name,
                PropertyAttributes.None,
                Signature.PropertyType,
                null);

            var setterSignature = Signature.GetSetMethod() ?? throw new MethodInfoException(Type, $"set_{Signature.Name}");
            var setter = Type.DefineMethod(
                setterSignature.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                null,
                new Type[] { Signature.PropertyType });
            var body = setter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var propertySignatureVariable);
            body.EmitLocalMethodSignatureVariable(out var methodSignatureVariable);

            body.EmitLocalPropertySetterValueFeatureVariable(out var propertySetterValueFeatureVariable);
            features.Add(propertySetterValueFeatureVariable);
            if (inParameters.Length > 0)
            {
                features.Add(body.EmitLocalParameterFeatureVariable<ParameterIn>());
            }

            body.EmitLocalInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetPropertySignature(Signature, propertySignatureVariable);
            body.EmitGetMethodSignature(setterSignature, methodSignatureVariable);

            body.EmitNewPropertySetterValueFeature(Signature, propertySignatureVariable, propertySetterValueFeatureVariable);
            if (inParameters.Length > 0)
            {
                body.EmitNewParameterFeature<ParameterIn>(methodSignatureVariable, parameters, features[1]);
            }

            body.EmitNewInvocation(invocationVariable, methodSignatureVariable, features);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            body.EmitReturnStatement();

            property.SetSetMethod(setter);
        }

        #endregion
    }
}