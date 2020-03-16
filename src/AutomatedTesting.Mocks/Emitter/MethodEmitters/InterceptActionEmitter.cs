namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IMethodEmitter"/> interface for emitting dynamic methods with
    /// return type <see cref="void"/> and without any out or ref input parameters that will forward any calls
    /// to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         Array.Empty<Type>());
    ///
    ///     var incovation = new Invocation(methodSignature);
    ///     _interceptor.Intercept(incovation);
    ///     return;
    /// ]]>
    ///
    /// or
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///
    ///     var incovation = new Invocation(methodSignature, parameterInFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class InterceptActionEmitter : MethodEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptActionEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptActionEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc />
        public override void EmitMethodImplementation()
        {
            var features = new List<LocalBuilder>();
            var parameters = Signature.GetParameters();
            var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();
            // ToDo: Ref/Out

            var method = Type.DefineMethod(
                Signature.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
                Signature.ReturnType,
                Signature.GetParameters().Select(p => p.ParameterType).ToArray());
            var body = method.GetILGenerator();

            // local variables
            body.EmitLocalMethodSignatureVariable(out var methodSignatureVariable);

            if (inParameters.Length > 0)
            {
                body.EmitLocalParameterInFeatureVariable(out var parameterInFeature);
                features.Add(parameterInFeature);
            }

            body.EmitLocalInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetMethodSignature(Signature, methodSignatureVariable);

            if (inParameters.Length > 0)
            {
                body.EmitNewParameterInFeature(methodSignatureVariable, parameters, features[0]);
            }

            body.EmitNewInvocation(invocationVariable, methodSignatureVariable, features);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            body.EmitReturnStatement();
        }

        #endregion
    }
}