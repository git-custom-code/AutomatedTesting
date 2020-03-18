namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
    using Extensions;
    using Interception;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property getters
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <typeparam name="T"> The type of the property. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(get_Property),
    ///         Array.Empty<Type>());
    ///
    ///     var propertyFeature = new PropertyInvocation(propertySignature);
    ///     var returnValueFeature = new ReturnValueInvocation<T>();
    ///
    ///     var incovation = new Invocation(methodSignature, propertyFeature, returnValueFeature);
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
    ///         nameof(get_Property),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var propertyFeature = new PropertyInvocation(propertySignature);
    ///     var returnValueFeature = new ReturnValueInvocation<T>();
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///
    ///     var incovation = new Invocation(methodSignature, propertyFeature, returnValueFeature, parameterInFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return returnValueFeature.ReturnValue;
    /// ]]>
    /// </remarks>
    public sealed class InterceptGetterEmitter<T> : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptGetterEmitter{T}"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptGetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base (type, signature, interceptorField)
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

            var getterSignature = Signature.GetGetMethod() ?? throw new MethodInfoException(Type, $"get_{Signature.Name}");
            var getter = Type.DefineMethod(
                getterSignature.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                Signature.PropertyType,
                null);
            var body = getter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var propertySignatureVariable);
            body.EmitLocalMethodSignatureVariable(out var methodSignatureVariable);

            body.EmitLocalPropertyFeatureVariable(out var propertyFeatureVariable);
            features.Add(propertyFeatureVariable);
            body.EmitLocalReturnValueFeatureVariable<T>(out var returnValueFeatureVariable);
            features.Add(returnValueFeatureVariable);
            if (inParameters.Length > 0)
            {
                body.EmitLocalParameterInFeatureVariable(out var parameterInFeature);
                features.Add(parameterInFeature);
            }

            body.EmitLocalInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetPropertySignature(Signature, propertySignatureVariable);
            body.EmitGetMethodSignature(getterSignature, methodSignatureVariable);

            body.EmitNewPropertyFeature(propertySignatureVariable, propertyFeatureVariable);
            body.EmitNewReturnValueFeature<T>(returnValueFeatureVariable);
            if (inParameters.Length > 0)
            {
                body.EmitNewParameterInFeature(methodSignatureVariable, parameters, features[1]);
            }

            body.EmitNewInvocation(invocationVariable, methodSignatureVariable, features);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            body.EmitReturnStatement<T>(returnValueFeatureVariable);

            property.SetGetMethod(getter);
        }

        #endregion
    }
}