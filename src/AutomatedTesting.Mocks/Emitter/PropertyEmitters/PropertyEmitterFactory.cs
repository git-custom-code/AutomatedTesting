namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Default implementation of the <see cref="IPropertyEmitterFactory"/> interface.
    /// </summary>
    public sealed class PropertyEmitterFactory : IPropertyEmitterFactory
    {
        #region Logic

        /// <inheritdoc />
        public IPropertyEmitter CreatePropertyEmitterFor(PropertyInfo signature, TypeBuilder type, FieldBuilder interceptor)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}