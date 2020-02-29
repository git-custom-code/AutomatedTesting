namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetMethod(string, Type[])"/> method.
        /// </summary>
        private static Lazy<MethodInfo> GetMethod { get; } = new Lazy<MethodInfo>(InitializeGetMethod, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="MethodBase.GetParameters()"/> method.
        /// </summary>
        private static Lazy<MethodInfo> GetParameters { get; } = new Lazy<MethodInfo>(InitializeGetParameters, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     MethodInfo methodSignature;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        public static void EmitLocalMethodSignatureVariable(this ILGenerator body, out LocalBuilder methodSignatureVariable)
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
        public static void EmitLocalParameterSignaturesVariable(this ILGenerator body, out LocalBuilder parameterSignaturesVariable)
        {
            parameterSignaturesVariable = body.DeclareLocal(typeof(ParameterInfo[]));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     methodSignature = typeof(Interface).GetMethod(nameof(Method), parameterTypes);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="signature"> The dynamic method's signature. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="Type"/> array variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        public static void EmitGetMethodSignature(
            this ILGenerator body,
            MethodInfo signature,
            LocalBuilder parameterTypesVariable,
            LocalBuilder methodSignatureVariable)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, signature.DeclaringType);
#pragma warning restore CS8604
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, signature.Name);
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
        public static void EmitGetParameterSignatures(
            this ILGenerator body,
            LocalBuilder methodSignatureVariable,
            LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, GetParameters.Value);
            body.Emit(OpCodes.Stloc, parameterSignaturesVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetMethod"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetMethod(string, System.Type[])"/> method. </returns>
        private static MethodInfo InitializeGetMethod()
        {
            var type = typeof(Type);
            var methodName = nameof(Type.GetMethod);
            var getMethod = @type.GetMethod(methodName, new[] { typeof(string), typeof(Type[]) });
            return getMethod ?? throw new MethodInfoException(type, methodName);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetParameters"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="MethodBase.GetParameters()"/> method. </returns>
        private static MethodInfo InitializeGetParameters()
        {
            var type = typeof(MethodInfo);
            var methodName = nameof(MethodInfo.GetParameters);
            var getParameters = @type.GetMethod(methodName, Array.Empty<Type>());
            return getParameters ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}