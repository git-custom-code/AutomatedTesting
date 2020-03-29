namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;

    /// <summary>
    /// Decorator type emitters can be used to dynamically create (decorator) types at runtime used as
    /// partial mock objects for automated testing.
    /// </summary>
    public interface ITypeDecoratorEmitter
    {
        /// <summary>
        /// Implement the public contract of the specified interface with signature of type <typeparamref name="T"/>,
        /// i.e. the dynamic decorator type will implement each property and/or method of the interface.
        /// </summary>
        /// <typeparam name="T"> The signature of the interface that should be implemented. </typeparam>
        void ImplementDecorator<T>() where T : class;

        /// <summary>
        /// Implement the public contract of the specified interface with the given <paramref name="signature"/>,
        /// i.e. the dynamic decorator type will implement each property and/or method of the interface.
        /// </summary>
        /// <param name="signature"> The signature of the interface that should be implemented. </param>
        void ImplementDecorator(Type signature);

        /// <summary>
        /// Convert the emitter instance to a standard .Net type by emitting it to an in-memory assembly.
        /// Note that once the type is emitted, no other changes can be made to it.
        /// </summary>
        /// <returns> The dynamically created type as .Net <see cref="Type"/>. </returns>
        Type ToType();
    }
}