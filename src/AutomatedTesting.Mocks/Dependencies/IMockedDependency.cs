namespace CustomCode.AutomatedTesting.Mocks.Dependencies
{
    using Arrangements;
    using Interception;

    /// <summary>
    /// Interface that represents a dependency that is replaced by a dynamically created mock with
    /// exactly the same signature.
    /// </summary>
    public interface IMockedDependency
    {
        /// <summary>
        /// Gets a collection of user made arrangements for intercepted method or property calls.
        /// </summary>
        IArrangementCollection Arrangements { get; }

        /// <summary>
        /// Gets an interceptor that is injected in mock and will execute the user's <see cref="Arrangements"/>.
        /// </summary>
        IInterceptor Interceptor { get; }

        /// <summary>
        /// Gets a dynamic proxy instance that has the exact same signature as the mocked dependency.
        /// </summary>
        object Instance { get; }
    }
}