namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;

    /// <summary>
    /// Dynamic proxy factory implementations can be used to create dynamic proxy types at runtime
    /// that will forward all method and/or property calls to injected <see cref="IInterceptor"/> instances.
    /// </summary>
    public interface IDynamicProxyFactory
    {
        /// <summary>
        /// Create a new dynamic proxy that implements the specified interface (of type <typeparamref name="T"/>)
        /// and forwards all method and/or property calls to the given <paramref name="interceptor"/>.
        /// </summary>
        /// <typeparam name="T"> The interface that should be implemented by the proxy. </typeparam>
        /// <param name="interceptor">
        /// The interceptor instance that will be injected and receive all proxy method and/or property calls.
        /// </param>
        /// <returns> The newly created dynamic proxy instance. </returns>
        T CreateForInterface<T>(IInterceptor interceptor) where T : notnull;

        /// <summary>
        /// Create a new dynamic proxy that implements the specified interface (of type <paramref name="interface"/>)
        /// and forwards all method and/or property calls to the given <paramref name="interceptor"/>.
        /// </summary>
        /// <param name="interface"> The interface that should be implemented by the proxy. </param>
        /// <param name="interceptor">
        /// The interceptor instance that will be injected and receive all proxy method and/or property calls.
        /// </param>
        /// <returns> The newly created dynamic proxy instance. </returns>
        object CreateForInterface(Type @interface, IInterceptor interceptor);
    }
}