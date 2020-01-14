namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Abstract base type for <see cref="IMethodEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class MethodEmitterBase : IMethodEmitter
    {
        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Type[] parameterTypes;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="System.Type"/> array variable. </param>
        protected void EmitLocalParameterTypesVariable(ILGenerator body, out LocalBuilder parameterTypesVariable)
        {
            parameterTypesVariable = body.DeclareLocal(typeof(Type[]));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     MethodInfo methodSignature;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        protected void EmitLocalMethodSignatureVariable(ILGenerator body, out LocalBuilder methodSignatureVariable)
        {
            methodSignatureVariable = body.DeclareLocal(typeof(MethodInfo));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ParameterInfo[] parameterSignatures;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterSignaturesVariable"> The emitted local <see cref="ParameterInfo"/> array variable. </param>
        protected void EmitLocalParameterSignaturesVariable(ILGenerator body, out LocalBuilder parameterSignaturesVariable)
        {
            parameterSignaturesVariable = body.DeclareLocal(typeof(ParameterInfo[]));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Dictionary<ParameterInfo, object> parameter;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The emitted local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        protected void EmitLocalParameterVariable(ILGenerator body, out LocalBuilder parameterVariable)
        {
            parameterVariable = body.DeclareLocal(typeof(Dictionary<ParameterInfo, object>));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     parameterTypes = new Type[N];
        ///     parameterTypes[0] = typeof(ParameterType1);
        ///     parameterTypes[1] = typeof(ParameterType2);
        ///     ...
        ///     parameterTypes[N-1] = typeof(ParameterTypeN);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="System.Type"/> array variable. </param>
        protected void EmitCreateParameterTypesArray(ILGenerator body, LocalBuilder parameterTypesVariable)
        {
            body.Emit(OpCodes.Nop);

            var parameters = Signature.GetParameters();
            body.Emit(OpCodes.Ldc_I4, parameters.Length);
            body.Emit(OpCodes.Newarr, typeof(Type));
            body.Emit(OpCodes.Stloc, parameterTypesVariable.LocalIndex);
            for (var i = 0u; i < parameters.Length; ++i)
            {
                body.Emit(OpCodes.Ldloc, parameterTypesVariable.LocalIndex);
                body.Emit(OpCodes.Ldc_I4, i);
                body.Emit(OpCodes.Ldtoken, parameters[i].ParameterType);
                body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
                body.Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     methodSignature = typeof(Interface).GetMethod(nameof(Method), parameterTypes);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="System.Type"/> array variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        protected void EmitGetMethodSignature(ILGenerator body, LocalBuilder parameterTypesVariable, LocalBuilder methodSignatureVariable)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, Signature.DeclaringType);
#pragma warning restore CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, Signature.Name);
            body.Emit(OpCodes.Ldloc, parameterTypesVariable.LocalIndex);
            body.Emit(OpCodes.Call, GetMethod.Value);
            body.Emit(OpCodes.Stloc, methodSignatureVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     parameterSignatures = methodSignature.GetParameters();
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="parameterSignaturesVariable"> The emitted local <see cref="ParameterInfo"/> array variable. </param>
        protected void EmitGetParameterSignatures(ILGenerator body, LocalBuilder methodSignatureVariable, LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, GetParameters.Value);
            body.Emit(OpCodes.Stloc, parameterSignaturesVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     parameter = new Dictionary<ParameterInfo, object>();
        ///     parameter.Add(parameterSignatures[0], parameterValue1);
        ///     parameter.Add(parameterSignatures[1], parameterValue2);
        ///     ...
        ///     parameter.Add(parameterSignatures[N-1], parameterValueN);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="parameterSignaturesVariable"> The emitted local <see cref="ParameterInfo"/> array variable. </param>
        protected void EmitCreateParameterDictionary(
            ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Newobj, DictionaryConstructor.Value);
            body.Emit(OpCodes.Stloc, parameterVariable.LocalIndex);

            var methodParameters = Signature.GetParameters();
            for (var i = 0u; i < methodParameters.Length; ++i)
            {
                var parameter = methodParameters[i];

                body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
                body.Emit(OpCodes.Ldloc, parameterSignaturesVariable.LocalIndex);
                body.Emit(OpCodes.Ldc_I4, i);
                body.Emit(OpCodes.Ldelem_Ref);

                body.Emit(OpCodes.Ldarg, i + 1);
                if (parameter.ParameterType.IsValueType)
                {
                    body.Emit(OpCodes.Box, parameter.ParameterType);
                }
                body.Emit(OpCodes.Callvirt, Add.Value);
            }
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     _interceptor.Intercept(invocation);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The local <see cref="ActionInvocation"/> variable. </param>
        protected void EmitCallInterceptor(ILGenerator body, LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, InterceptorField);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, Intercept.Value);
        }

        #endregion
    }
}