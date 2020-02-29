namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Abstract base type for <see cref="IPropertyEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class PropertyEmitterBase
    {
        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyInfo propertySignature;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        protected void EmitLocalPropertySignatureVariable(ILGenerator body, out LocalBuilder propertySignatureVariable)
        {
            propertySignatureVariable = body.DeclareLocal(typeof(PropertyInfo));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     propertySignature = typeof(Interface).GetProperty(nameof(Property));
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        protected void EmitGetPropertySignature(ILGenerator body, LocalBuilder propertySignatureVariable)
        {
            body.Emit(OpCodes.Nop);
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, Signature.DeclaringType);
#pragma warning restore CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, Signature.Name);
            body.Emit(OpCodes.Call, GetProperty.Value);
            body.Emit(OpCodes.Stloc, propertySignatureVariable.LocalIndex);
        }

        #endregion
    }
}