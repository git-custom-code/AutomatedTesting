namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Abstract base type for <see cref="IPropertyEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class PropertyDecoratorEmitterBase : IPropertyEmitter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyDecoratorEmitterBase"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        protected PropertyDecoratorEmitterBase(
            TypeBuilder type,
            PropertyInfo signature,
            FieldBuilder decorateeField,
            FieldBuilder interceptorField)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            DecorateeField = decorateeField ?? throw new ArgumentNullException(nameof(decorateeField));
            InterceptorField = interceptorField ?? throw new ArgumentNullException(nameof(interceptorField));
        }

        /// <summary>
        /// Gets The <see cref="Type"/>'s decoratee backing field.
        /// </summary>
        protected FieldBuilder DecorateeField { get; }

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

        /// <inheritdoc cref="IPropertyEmitter" />
        public abstract void EmitPropertyImplementation();

        #endregion
    }
}