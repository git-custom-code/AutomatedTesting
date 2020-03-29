namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Logic

        /// <summary>
        /// </summary>
        /// <param name="body"> The body of the dynamic method or property. </param>
        /// <param name="signature"> The signature of the method to be called on the decorated instance. </param>
        /// <param name="parameterSignatures"> The signatures of the method's parameters. </param>
        /// <param name="decorateeField"> The backing field that holds the decorated instance. </param>
        /// <param name="passSetterValue"> True to pass the value of a property setter, false otherwise. </param>
        /// <remarks>
        /// Emits the following source code:
        ///
        /// <![CDATA[
        ///     _decorate.Method(parameter1, ... parameterN);
        /// ]]>
        /// </remarks>
        public static void EmitDecorateeCall(
            this ILGenerator body,
            MethodInfo signature,
            ParameterInfo[] parameterSignatures,
            FieldBuilder decorateeField,
            bool passSetterValue = false)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, decorateeField);

            for (var i = 0; i < parameterSignatures.Length; ++i)
            {
                body.Emit(OpCodes.Ldarg, parameterSignatures[i].Position + 1);
            }

            if (passSetterValue)
            {
                body.Emit(OpCodes.Ldarg, parameterSignatures.Length + 1);
            }

            body.Emit(OpCodes.Callvirt, signature);
        }

        #endregion
    }
}