namespace CustomCode.AutomatedTesting.Mocks.Dependencies
{
    using Arrangements;
    using Interception;
    using System;

    /// <summary>
    /// Default implementation of the <see cref="IMockedDependency{T}"/> and <see cref="IMockedDependency"/>
    /// interfaces.
    /// </summary>
    /// <typeparam name="T"> The signature of the mocked dependency (must be an interface type). </typeparam>
    public class MockedDependency<T> : IMockedDependency<T>
        where T : notnull
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="MockedDependency{T}"/> type.
        /// </summary>
        /// <param name="arrangements">
        /// A collection of user made arrangements for intercepted method or property calls.
        /// </param>
        /// <param name="interceptor">
        /// An interceptor that is injected in mock and will execute the user's <paramref name="arrangements"/>.
        /// </param>
        /// <param name="instance">
        /// A dynamic proxy instance that has the exact same signature as the mocked dependency.
        /// </param>
        public MockedDependency(IArrangementCollection arrangements, IInterceptor interceptor, T instance)
        {
            Arrangements = arrangements;
            Interceptor = interceptor;
            Instance = instance;
            Signature = typeof(T);
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public IArrangementCollection Arrangements { get; }

        /// <inheritdoc />
        public IInterceptor Interceptor { get; }

        /// <inheritdoc />
        public T Instance { get; }

        /// <inheritdoc />
        object IMockedDependency.Instance
        {
            get { return Instance; }
        }

        /// <inheritdoc />
        public Type Signature { get; }

        #endregion
    }
}