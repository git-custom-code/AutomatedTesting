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
        /// Gets the cached signature of the <see cref="Array.Empty{T}"/> static method.
        /// </summary>
        private static Lazy<MethodInfo> ArrayEmptyType { get; }
            = new Lazy<MethodInfo>(InitializeArrayEmptyType, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits a local <see cref="MethodInfo"/> variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     MethodInfo methodSignature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalMethodSignatureVariable(this ILGenerator body, out LocalBuilder methodSignatureVariable)
        {
            methodSignatureVariable = body.DeclareLocal(typeof(MethodInfo));
        }

        /// <summary>
        /// Emits a call to <see cref="Type.GetMethod(string, Type[])"/> in order to get the signature of the invoked method.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="signature"> The dynamic method's signature. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     methodSignature = typeof(Interface).GetMethod(nameof(Method), new[] { typeof(parameter1), ... typeof(parameterN) });
        /// ]]>
        /// </remarks>
        public static void EmitGetMethodSignature(
            this ILGenerator body,
            MethodInfo signature,
            LocalBuilder methodSignatureVariable)
        {
            // nameof(Method)
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, signature.DeclaringType);
#pragma warning restore CS8604
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, signature.Name);

            var parameters = signature.GetParameters();
            if (parameters.Length == 0)
            {
                // Array.Empty<Type>()
                body.Emit(OpCodes.Call, ArrayEmptyType.Value);
            }
            else
            {
                // new[] { typeof(parmeter1), ... typeof(parameterN) }
                body.Emit(OpCodes.Ldc_I4, parameters.Length);
                body.Emit(OpCodes.Newarr, typeof(Type));

                for (var i = 0u; i < parameters.Length; ++i)
                {
                    body.Emit(OpCodes.Dup);
                    body.Emit(OpCodes.Ldc_I4, i);
                    body.Emit(OpCodes.Ldtoken, parameters[i].ParameterType);
                    body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
                    body.Emit(OpCodes.Stelem_Ref);
                }
            }

            // methodSignature = GetMethod
            body.Emit(OpCodes.Call, GetMethod.Value);
            body.Emit(OpCodes.Stloc, methodSignatureVariable.LocalIndex);
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
        /// Initialization logic for the <see cref="ArrayEmptyType"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Array.Empty{T}"/> method. </returns>
        private static MethodInfo InitializeArrayEmptyType()
        {
            var type = typeof(Array);
            var methodName = nameof(Array.Empty);
            var openGenericArrayEmpty = @type.GetMethod(methodName, 1, Array.Empty<Type>());
            if (openGenericArrayEmpty == null)
            {
                throw new MethodInfoException(type, methodName);
            }
            var arrayEmpty = openGenericArrayEmpty.MakeGenericMethod(typeof(Type));
            return arrayEmpty ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}