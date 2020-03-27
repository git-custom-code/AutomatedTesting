namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// This factory can create <see cref="IMethodEmitter"/> instances based on the reflected signature of a method
    /// (see <see cref="IMethodEmitter"/> interface for more details).
    /// </summary>
    public interface IMethodDecoratorEmitterFactory
    {
        /// <summary>
        /// Creates a new <see cref="IMethodEmitter"/> instance for a given method with the specified <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">
        /// The signature of the method that should be dynamically created by the resulting <see cref="IMethodEmitter"/>.
        /// </param>
        /// <param name="type"> The dynamic type that will contain the dynamic method to be created. </param>
        /// <param name="decoratee"> A field that contains the decorated instance. </param>
        /// <param name="interceptor">
        /// The dynamic <paramref name="type"/>'s <see cref="Interception.IInterceptor"/> that should be called by the dynamic method.
        /// </param>
        /// <returns>
        /// A new <see cref="IMethodEmitter"/> instance that can dynamically create a dynamic method with the given
        /// <paramref name="signature"/> for the dynamic <paramref name="type"/>.
        /// </returns>
        IMethodEmitter CreateMethodDecoratorEmitterFor(
            MethodInfo signature,
            TypeBuilder type,
            FieldBuilder decoratee,
            FieldBuilder interceptor);
    }
}