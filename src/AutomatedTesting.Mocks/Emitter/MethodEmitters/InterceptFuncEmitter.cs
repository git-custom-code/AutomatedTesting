namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IMethodEmitter"/> interface for emitting dynamic methods with
    /// a non-void return type and without any out or ref input parameters that will forward any calls
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
    ///     var invocation = new FuncInvocation(parameter, methodSignature);
    ///     _interceptor.Intercept(invocation);
    ///
    ///     return (ReturnType)invocation.ReturnValue;
    /// ]]>
    /// </remarks>
    public sealed class InterceptFuncEmitter : MethodEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptFuncEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptFuncEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="FuncInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> FuncInvocationConstructor { get; } = new Lazy<ConstructorInfo>(InitializeFuncInvocationConstructor, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="FuncInvocation.ReturnValue"/> getter.
        /// </summary>
        private static Lazy<MethodInfo> GetReturnValue { get; } = new Lazy<MethodInfo>(InitializeGetReturnValue, true);

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
            EmitLocalReturnValue(body, out var returnValue);

            // body
            EmitCreateParameterTypesArray(body, parameterTypesVariable);
            EmitGetMethodSignature(body, parameterTypesVariable, methodSignatureVariable);
            EmitGetParameterSignatures(body, methodSignatureVariable, parameterSignaturesVariable);
            EmitCreateParameterDictionary(body, parameterVariable, parameterSignaturesVariable);
            EmitNewFuncInvocation(body, parameterVariable, methodSignatureVariable, invocationVariable);
            EmitCallInterceptor(body, invocationVariable);

            EmitReturnStatement(body, invocationVariable, returnValue);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     FuncInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="FuncInvocation"/> variable. </param>
        private void EmitLocalInvocationVariable(ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(FuncInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ReturnValueType returnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="returnValue"> The emitted local return value. </param>
        private void EmitLocalReturnValue(ILGenerator body, out LocalBuilder returnValue)
        {
            returnValue = body.DeclareLocal(Signature.ReturnType);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new FuncInvocation(parameter, methodSignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="FuncInvocation"/> variable. </param>
        private void EmitNewFuncInvocation(
            ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder methodSignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, FuncInvocationConstructor.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     return (ReturnType)invocation.ReturnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The local <see cref="FuncInvocation"/> variable. </param>
        private void EmitReturnStatement(ILGenerator body, LocalBuilder invocationVariable, LocalBuilder returnValue)
        {
            var label = body.DefineLabel();

            body.Emit(OpCodes.Nop);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, GetReturnValue.Value);
            if (Signature.ReturnType.IsValueType)
            {
                body.Emit(OpCodes.Unbox_Any, Signature.ReturnType);
            }
            else
            {
                body.Emit(OpCodes.Castclass, Signature.ReturnType);
            }
            body.Emit(OpCodes.Stloc, returnValue.LocalIndex);
            body.Emit(OpCodes.Br_S, label);
            body.MarkLabel(label);
            body.Emit(OpCodes.Ldloc, returnValue.LocalIndex);
            body.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Initialization logic for the <see cref="FuncInvocationConstructor"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="FuncInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeFuncInvocationConstructor()
        {
            var constructor = typeof(FuncInvocation).GetConstructor(new[] { typeof(IDictionary<ParameterInfo, object>), typeof(MethodInfo) });
            return constructor ?? throw new ArgumentNullException(nameof(FuncInvocationConstructor));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetReturnValue"/> property.
        /// </summary>
        /// <returns> Thethe signature of the <see cref="FuncInvocation.ReturnValue"/> getter. </returns>
        private static MethodInfo InitializeGetReturnValue()
        {
            var getReturnValue = typeof(FuncInvocation).GetProperty(nameof(FuncInvocation.ReturnValue))?.GetGetMethod();
            return getReturnValue ?? throw new ArgumentNullException(nameof(GetReturnValue));
        }

        #endregion
    }
}