namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
    using System.Linq;
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
            if (signature.GetIndexParameters().Any())
            {
                throw new NotSupportedException();
            }

            if (signature.CanRead)
            {
                return new InterceptGetterEmitter(type, signature, interceptor);
            }
            else if (signature.CanWrite)
            {
                return new InterceptSetterEmitter(type, signature, interceptor);
            }

            throw new NotSupportedException();
        }

        #endregion
    }
}