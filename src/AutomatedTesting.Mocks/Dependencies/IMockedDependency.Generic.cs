namespace CustomCode.AutomatedTesting.Mocks.Dependencies
{
    /// <summary>
    /// Generic version of the <see cref="IMockedDependency"/> interface.
    /// </summary>
    /// <typeparam name="T"> The signature of the mocked dependency (must be an interface type). </typeparam>
    public interface IMockedDependency<T> : IMockedDependency
        where T : notnull
    {
        /// <summary>
        /// Gets a dynamic proxy instance that has the exact same signature as the mocked dependency.
        /// </summary>
        new T Instance { get; }
    }
}