namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Logic

        /// <summary>
        /// Emits a single return statement for an intercepted void method.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     return;
        /// ]]>
        /// </remarks>
        public static void EmitReturnStatement(this ILGenerator body)
        {
            body.Emit(OpCodes.Ret);
        }

        #endregion
    }
}