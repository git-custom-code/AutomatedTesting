namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception.Parameters;
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
        /// Gets the cached signature of the <see cref="ParameterIn"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateParameterIn { get; }
            = new Lazy<ConstructorInfo>(InitializeCreateParameterIn, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ParameterIn parameterInFeature;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterInFeatureVariable"> The emitted local <see cref="ParameterIn"/> variable. </param>
        public static void EmitLocalParameterInFeatureVariable(
            this ILGenerator body, out LocalBuilder parameterInFeatureVariable)
        {
            parameterInFeatureVariable = body.DeclareLocal(typeof(ParameterIn));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     parameterInFeature = new ParameterIn(methodSignature, new[] { ParamValue1, ... ParamValueN });
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="parameterSignatures"> The dynamic method's parameter signatures. </param>
        /// <param name="paramterInFeatureVariable"> The emitted local <see cref="ParameterIn"/> variable. </param>
        public static void EmitNewParameterInFeature(
            this ILGenerator body,
            LocalBuilder methodSignatureVariable,
            ParameterInfo[] parameterSignatures,
            LocalBuilder paramterInFeatureVariable)
        {
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Ldc_I4, parameterSignatures.Length);
            body.Emit(OpCodes.Newarr, typeof(object));
            for (var i=0; i< parameterSignatures.Length; ++i)
            {
                var parameter = parameterSignatures[i];
                body.Emit(OpCodes.Dup);
                body.Emit(OpCodes.Ldc_I4, i);
                body.Emit(OpCodes.Ldarg, i + 1);
                if (parameter.ParameterType.IsValueType)
                {
                    body.Emit(OpCodes.Box, parameter.ParameterType);
                }
                body.Emit(OpCodes.Stelem_Ref);
            }
            body.Emit(OpCodes.Newobj, CreateParameterIn.Value);
            body.Emit(OpCodes.Stloc, paramterInFeatureVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateParameterIn"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="ParameterIn"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateParameterIn()
        {
            var type = typeof(ParameterIn);
            var constructor = type.GetConstructor(new[] { typeof(MethodInfo), typeof(object?[]) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}