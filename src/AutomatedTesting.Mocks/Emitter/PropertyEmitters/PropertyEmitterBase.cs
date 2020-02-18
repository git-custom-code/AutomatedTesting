namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Abstract base type for <see cref="IPropertyEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class PropertyEmitterBase : IPropertyEmitter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyEmitterBase"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        protected PropertyEmitterBase(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
        {
            Type = type;
            Signature = signature;
            InterceptorField = interceptorField;
        }

        /// <summary>
        /// Gets the <see cref="Type"/>'s <see cref="IInterceptor"/> backing field.
        /// </summary>
        protected FieldBuilder InterceptorField { get; }

        /// <summary>
        /// Gets the signature of the property to be created.
        /// </summary>
        protected PropertyInfo Signature { get; }

        /// <summary>
        /// Gets the dynamic proxy type.
        /// </summary>
        protected TypeBuilder Type { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public abstract void EmitPropertyImplementation();

        #endregion
    }
}