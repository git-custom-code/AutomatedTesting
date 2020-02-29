namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
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
    /// <![CDATA[
    ///     var parameterTypes = new[] { typeof(ParameterType1), ... , typeof(ParmameterTypeN) };
    ///     var methodSignature = typeof(Interface).GetMethod(nameof(Method), parameterTypes);
    ///     var parameterSignatures = methodSignature.GetParameters();
    ///
    ///     var parameter = new Dictionary<ParameterInfo, object>();
    ///     parameter.Add(parameterSignatures[0], parameterValue1);
    ///     ...
    ///     parameter.Add(parameterSignatures[N-1], parameterValueN);
    ///
    ///     var invocation = new ActionInvocation(parameter, methodSignature);
    ///     _interceptor.Intercept(invocation);
    ///
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
            var method = Type.DefineMethod(
                Signature.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
                Signature.ReturnType,
                Signature.GetParameters().Select(p => p.ParameterType).ToArray());
            var body = method.GetILGenerator();

            // local variables
            body.EmitLocalParameterTypesVariable(out var parameterTypesVariable);
            body.EmitLocalMethodSignatureVariable(out var methodSignatureVariable);
            body.EmitLocalParameterSignaturesVariable(out var parameterSignaturesVariable);
            body.EmitLocalParameterVariable(out var parameterVariable);
            body.EmitLocalActionInvocationVariable(out var invocationVariable);

            // body
            body.EmitCreateParameterTypesArray(Signature, parameterTypesVariable);
            body.EmitGetMethodSignature(Signature, parameterTypesVariable, methodSignatureVariable);
            body.EmitGetParameterSignatures(methodSignatureVariable, parameterSignaturesVariable);
            body.EmitCreateParameterDictionary(Signature, parameterVariable, parameterSignaturesVariable);
            body.EmitNewActionInvocation(parameterVariable, methodSignatureVariable, invocationVariable);
            body.EmitInterceptCall(InterceptorField, invocationVariable);

            EmitReturnStatement(body);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     return;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        private void EmitReturnStatement(ILGenerator body)
        {
            body.Emit(OpCodes.Nop);
            body.Emit(OpCodes.Ret);
        }

        #endregion
    }
}