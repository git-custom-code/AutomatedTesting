namespace CustomCode.AutomatedTesting.Mocks
{
    using Dependencies;
    using Fluent;

    /// <summary>
    /// Create a new instance of type <typeparamref name="T"/> and replace any dependencies with
    /// mocked instances instead.
    /// </summary>
    /// <typeparam name="T">
    /// The signature of the type under test whose dependencies are replaced by mocks
    /// (see <see cref="IMockedDependency"/> for more details).
    /// </typeparam>
    public interface IMocked<T>
        where T : class
    {
        /// <summary>
        /// Gets the instance of the type under test whose dependencies are replaced by mocks.
        /// </summary>
        T Instance { get; }

        /// <summary>
        /// Setup the behavior of a mocked dependency.
        /// </summary>
        /// <typeparam name="TMock"> The type of the mocked dependecy. </typeparam>
        /// <returns> An <see cref="IMockBehavior{TMock}"/> instance that can be used to setup the behavior. </returns>
        IMockBehavior<TMock> ArrangeFor<TMock>() where TMock : class; 
    }
}