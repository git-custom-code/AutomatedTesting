namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using System;
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

        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="ActionInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> ActionInvocationConstructor { get; } = new Lazy<ConstructorInfo>(InitializeActionInvocationConstructor, true);

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
            EmitLocalParameterTypesVariable(body, out var parameterTypesVariable);
            EmitLocalMethodSignatureVariable(body, out var methodSignatureVariable);
            EmitLocalParameterSignaturesVariable(body, out var parameterSignaturesVariable);
            EmitLocalParameterVariable(body, out var parameterVariable);
            EmitLocalInvocationVariable(body, out var invocationVariable);

            // body
            EmitCreateParameterTypesArray(body, parameterTypesVariable);
            EmitGetMethodSignature(body, parameterTypesVariable, methodSignatureVariable);
            EmitGetParameterSignatures(body, methodSignatureVariable, parameterSignaturesVariable);
            EmitCreateParameterDictionary(body, parameterVariable, parameterSignaturesVariable);
            EmitNewActionInvocation(body, parameterVariable, methodSignatureVariable, invocationVariable);
            body.EmitInterceptCall(InterceptorField, invocationVariable);

            EmitReturnStatement(body);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ActionInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="ActionInvocation"/> variable. </param>
        private void EmitLocalInvocationVariable(ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(ActionInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new ActionInvocation(parameter, methodSignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="ActionInvocation"/> variable. </param>
        private void EmitNewActionInvocation(
            ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder methodSignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, ActionInvocationConstructor.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
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

        /// <summary>
        /// Initialization logic for the <see cref="ActionInvocationConstructor"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="ActionInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeActionInvocationConstructor()
        {
            var constructor = typeof(ActionInvocation).GetConstructor(new[] { typeof(IDictionary<ParameterInfo, object>), typeof(MethodInfo) });
            return constructor ?? throw new ArgumentNullException(nameof(ActionInvocationConstructor));
        }

        #endregion
    }
}