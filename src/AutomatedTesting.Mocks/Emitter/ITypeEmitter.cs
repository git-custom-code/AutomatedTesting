namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;

    /// <summary>
    /// Type emitters can be used to dynamical (proxy) types at runtime that can be used by automated testing
    /// as mock objects.
    /// </summary>
    public interface ITypeEmitter
    {
        /// <summary>
        /// Implement the public contract of the specified interface with signature of type <typeparamref name="T"/>,
        /// i.e. the dynamic proxy type will implement each property and/or method of the interface.
        /// </summary>
        /// <typeparam name="T"> The signature of the interface that should be implemented. </typeparam>
        void ImplementInterface<T>() where T : class;

        /// <summary>
        /// Implement the public contract of the specified interface with signature of type <paramref name="interface"/>,
        /// i.e. the dynamic proxy type will implement each property and/or method of the interface.
        /// </summary>
        /// <param name="interface"> The signature of the interface that should be implemented. </param>
        void ImplementInterface(Type @interface);

        /// <summary>
        /// Convert the emitter instance to a standard .Net type by emitting it to an in-memory assembly.
        /// Note that once the type is emitted, no other changes can be made to it.
        /// </summary>
        /// <returns> The dynamically created type as .Net <see cref="Type"/>. </returns>
        Type ToType();
    }
}