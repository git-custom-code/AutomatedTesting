namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// This factory can create <see cref="IPropertyEmitter"/> instances based on the reflected signature of a property
    /// (see <see cref="IPropertyEmitter"/> interface for more details).
    /// </summary>
    public interface IPropertyDecoratorEmitterFactory
    {
        /// <summary>
        /// Creates a new <see cref="IPropertyEmitter"/> instance for a given property with the specified <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">
        /// The signature of the property that should be dynamically created by the resulting <see cref="IPropertyEmitter"/>.
        /// </param>
        /// <param name="type"> The dynamic type that will contain the dynamic property to be created. </param>
        /// <param name="decoratee"> A field that contains the decorated instance. </param>
        /// <param name="interceptor">
        /// The dynamic <paramref name="type"/>'s <see cref="Interception.IInterceptor"/> that should be called by the dynamic property.
        /// </param>
        /// <returns>
        /// A new <see cref="IPropertyEmitter"/> instance that can dynamically create a dynamic property with the given
        /// <paramref name="signature"/> for the dynamic <paramref name="type"/>.
        /// </returns>
        IPropertyEmitter CreatePropertyEmitterFor(
            PropertyInfo signature,
            TypeBuilder type,
            FieldBuilder decoratee,
            FieldBuilder interceptor);
    }
}