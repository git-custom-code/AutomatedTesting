namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Default implementation of the <see cref="IPropertyDecoratorEmitterFactory"/> interface.
    /// </summary>
    public sealed class PropertyDecoratorEmitterFactory : IPropertyDecoratorEmitterFactory
    {
        #region Logic

        /// <inheritdoc />
        public IPropertyEmitter CreatePropertyEmitterFor(
            PropertyInfo signature,
            TypeBuilder type,
            FieldBuilder decoratee,
            FieldBuilder interceptor)
        {
            if (signature.CanWrite)
            {
                return new DecorateSetterEmitter(type, signature, decoratee, interceptor);
            }

            throw new NotSupportedException();
        }

        #endregion
    }
}