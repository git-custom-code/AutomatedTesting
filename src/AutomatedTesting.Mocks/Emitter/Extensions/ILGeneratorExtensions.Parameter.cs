namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> method.
        /// </summary>
        private static Lazy<MethodInfo> Add { get; } = new Lazy<MethodInfo>(InitialzeAdd, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateDictionary { get; } = new Lazy<ConstructorInfo>(InitializeCreateDictionary, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Type[] parameterTypes;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="Type"/> array variable. </param>
        public static void EmitLocalParameterTypesVariable(this ILGenerator body, out LocalBuilder parameterTypesVariable)
        {
            parameterTypesVariable = body.DeclareLocal(typeof(Type[]));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Dictionary<ParameterInfo, object?> parameter;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The emitted local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        public static void EmitLocalParameterVariable(this ILGenerator body, out LocalBuilder parameterVariable)
        {
            parameterVariable = body.DeclareLocal(typeof(Dictionary<ParameterInfo, object?>));
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
        /// <param name="signature"> The dynamic method's signature. </param>
        /// <param name="parameterTypesVariable"> The emitted local <see cref="Type"/> array variable. </param>
        public static void EmitCreateParameterTypesArray(
            this ILGenerator body,
            MethodInfo signature,
            LocalBuilder parameterTypesVariable)
        {
            body.Emit(OpCodes.Nop);

            var parameters = signature.GetParameters();
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
        ///     parameter = new Dictionary<ParameterInfo, object?>();
        ///     parameter.Add(parameterSignatures[0], parameterValue1);
        ///     parameter.Add(parameterSignatures[1], parameterValue2);
        ///     ...
        ///     parameter.Add(parameterSignatures[N-1], parameterValueN);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="signature"> The dynamic method's signature. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="parameterSignaturesVariable"> The emitted local <see cref="ParameterInfo"/> array variable. </param>
        public static void EmitCreateParameterDictionary(
            this ILGenerator body,
            MethodInfo signature,
            LocalBuilder parameterVariable,
            LocalBuilder parameterSignaturesVariable)
        {
            body.Emit(OpCodes.Newobj, CreateDictionary.Value);
            body.Emit(OpCodes.Stloc, parameterVariable.LocalIndex);

            var methodParameters = signature.GetParameters();
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
        /// Initialization logic for the <see cref="Add"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> method. </returns>
        private static MethodInfo InitialzeAdd()
        {
            var type = typeof(Dictionary<ParameterInfo, object?>);
            var methodName = nameof(Dictionary<ParameterInfo, object?>.Add);
            var add = type.GetMethod(methodName);
            return add ?? throw new MethodInfoException(type, methodName);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateDictionary"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Dictionary{TKey, TValue}"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateDictionary()
        {
            var type = typeof(Dictionary<ParameterInfo, object?>);
            var constructor = type.GetConstructor(Array.Empty<Type>());
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}