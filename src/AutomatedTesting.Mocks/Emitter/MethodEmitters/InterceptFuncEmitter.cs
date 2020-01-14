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
    public sealed class InterceptFuncEmitter : IMethodEmitter
    {
        #region Dependencies

        /// <summary>
        /// Static <see cref="InterceptFuncEmitter"/> constructor.
        /// </summary>
        static InterceptFuncEmitter()
        {
            var getTypeFromHandle = typeof(Type).GetMethod(
                nameof(System.Type.GetTypeFromHandle),
                BindingFlags.Static | BindingFlags.Public);
            GetTypeFromHandle = getTypeFromHandle ?? throw new ArgumentNullException(nameof(GetTypeFromHandle));

            var getMethod = typeof(Type).GetMethod(
                nameof(System.Type.GetMethod),
                new[] { typeof(string), typeof(Type[]) });
            GetMethod = getMethod ?? throw new ArgumentNullException(nameof(GetMethod));

            var getParameters = typeof(MethodInfo).GetMethod(nameof(MethodInfo.GetParameters), new Type[0]);
            GetParameters = getParameters ?? throw new ArgumentNullException(nameof(GetParameters));

            var add = typeof(Dictionary<string, object>).GetMethod(nameof(Dictionary<ParameterInfo, object>.Add));
            Add = add ?? throw new ArgumentNullException(nameof(Add));

            var constructor = typeof(Dictionary<ParameterInfo, object>).GetConstructor(new Type[0]);
            DictionaryConstructor = constructor ?? throw new ArgumentNullException(nameof(DictionaryConstructor));

            constructor = typeof(FuncInvocation).GetConstructor(new[] { typeof(IDictionary<ParameterInfo, object>), typeof(MethodInfo) });
            FuncInvocationConstructor = constructor ?? throw new ArgumentNullException(nameof(FuncInvocationConstructor));

            var intercept = typeof(IInterceptor).GetMethod(
                nameof(IInterceptor.Intercept),
                BindingFlags.Public | BindingFlags.Instance);
            Intercept = intercept ?? throw new ArgumentNullException(nameof(Intercept));

            var getReturnValue = typeof(FuncInvocation).GetProperty(nameof(FuncInvocation.ReturnValue))?.GetGetMethod();
            GetReturnValue = getReturnValue ?? throw new ArgumentNullException(nameof(getReturnValue));

        }

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptFuncEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptFuncEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
        {
            Type = type;
            Signature = signature;
            InterceptorField = interceptorField;
        }

        /// <summary>
        /// Gets The <see cref="Type"/>'s <see cref="IInterceptor"/> backing field.
        /// </summary>
        private FieldBuilder InterceptorField { get; }

        /// <summary>
        /// Gets the signature of the method to be created.
        /// </summary>
        private MethodInfo Signature { get; }

        /// <summary>
        /// Gets the dynamic proxy type.
        /// </summary>
        private TypeBuilder Type { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="FuncInvocation"/> constructor.
        /// </summary>
        private static ConstructorInfo FuncInvocationConstructor { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> method.
        /// </summary>
        private static MethodInfo Add { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}"/> constructor.
        /// </summary>
        private static ConstructorInfo DictionaryConstructor { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetMethod(string, System.Type[])"/> method.
        /// </summary>
        private static MethodInfo GetMethod { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="MethodBase.GetParameters()"/> method.
        /// </summary>
        private static MethodInfo GetParameters { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="System.Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).
        /// </summary>
        private static MethodInfo GetTypeFromHandle { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method.
        /// </summary>
        private static MethodInfo Intercept { get; }

        /// <summary>
        /// Gets the cached signature of the <see cref="FuncInvocation.ReturnValue"/> getter.
        /// </summary>
        private static MethodInfo GetReturnValue { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void EmitMethodImplementation()
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

        #region Local Variables

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Type[] parameterTypes;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="System.Type"/> array variable. </param>
        private void EmitLocalParameterTypesVariable(ILGenerator body, out LocalBuilder parameterTypesVariable)
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
        private void EmitLocalMethodSignatureVariable(ILGenerator body, out LocalBuilder methodSignatureVariable)
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
        private void EmitLocalParameterSignaturesVariable(ILGenerator body, out LocalBuilder parameterSignaturesVariable)
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
        private void EmitLocalParameterVariable(ILGenerator body, out LocalBuilder parameterVariable)
        {
            parameterVariable = body.DeclareLocal(typeof(Dictionary<ParameterInfo, object>));
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
        ///     ReturnValueType returnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="returnValue"> The emitted local return value. </param>
        private void EmitLocalReturnValue(ILGenerator body, out LocalBuilder returnValue)
        {
            returnValue = body.DeclareLocal(Signature.ReturnType);
        }

        #endregion

        #region Body

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
        private void EmitCreateParameterTypesArray(ILGenerator body, LocalBuilder parameterTypesVariable)
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
                body.Emit(OpCodes.Call, GetTypeFromHandle);
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
        private void EmitGetMethodSignature(ILGenerator body, LocalBuilder parameterTypesVariable, LocalBuilder methodSignatureVariable)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, Signature.DeclaringType);
#pragma warning restore CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Call, GetTypeFromHandle);
            body.Emit(OpCodes.Ldstr, Signature.Name);
            body.Emit(OpCodes.Ldloc, parameterTypesVariable.LocalIndex);
            body.Emit(OpCodes.Call, GetMethod);
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
        private void EmitGetParameterSignatures(ILGenerator body, LocalBuilder methodSignatureVariable, LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, GetParameters);
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
        private void EmitCreateParameterDictionary(
            ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Newobj, DictionaryConstructor);
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
                body.Emit(OpCodes.Callvirt, Add);
            }
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
            body.Emit(OpCodes.Newobj, FuncInvocationConstructor);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     _interceptor.Intercept(invocation);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The local <see cref="FuncInvocation"/> variable. </param>
        private void EmitCallInterceptor(ILGenerator body, LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, InterceptorField);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, Intercept);
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
            body.Emit(OpCodes.Callvirt, GetReturnValue);
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

        #endregion

        #endregion
    }
}