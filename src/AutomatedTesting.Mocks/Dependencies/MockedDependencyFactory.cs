namespace CustomCode.AutomatedTesting.Mocks.Dependencies
{
    using Arrangements;
    using Emitter;
    using Interception;
    using System;

    /// <summary>
    /// Default implementation of the <see cref="IMockedDependencyFactory"/> interface.
    /// </summary>
    public sealed class MockedDependencyFactory : IMockedDependencyFactory
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="MockedDependencyFactory"/> type.
        /// </summary>
        /// <param name="dynamicProxyFactory"> A factory that can create a dynamic proxy for a given dependency. </param>
        /// <param name="interceptorFactory"> A factory that can create an interceptor for a given dependency. </param>
        public MockedDependencyFactory(IDynamicProxyFactory dynamicProxyFactory, IInterceptorFactory interceptorFactory)
        {
            DynamicProxyFactory = dynamicProxyFactory ?? throw new ArgumentNullException(nameof(dynamicProxyFactory));
            InterceptorFactory = interceptorFactory ?? throw new ArgumentNullException(nameof(interceptorFactory));
        }

        /// <summary>
        /// Gets a factory that can create a dynamic proxy for a given dependency.
        /// </summary>
        private IDynamicProxyFactory DynamicProxyFactory { get; }

        /// <summary>
        /// Gets a factory that can create an interceptor for a given dependency.
        /// </summary>
        private IInterceptorFactory InterceptorFactory { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="IMockedDependencyFactory" />
        public IMockedDependency CreateDecoratedDependency(Type dependency, object decoratee)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }
            else if (!dependency.IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{dependency.FullName}'");
            }

            var arrangements = new ArrangementCollection();
            var interceptor = InterceptorFactory.CreateInterceptorFor(MockBehavior.Partial, arrangements);
            var proxy = DynamicProxyFactory.CreateDecorator(dependency, decoratee, interceptor);
            var mockedDependencyType = typeof(MockedDependency<>).MakeGenericType(dependency);
            var instance = Activator.CreateInstance(mockedDependencyType, new[] { arrangements, interceptor, proxy });
            if (instance is IMockedDependency mockedDependency)
            {
                return mockedDependency;
            }

            throw new InvalidOperationException("Invalid mocked dependency instance");
        }

        /// <inheritdoc cref="IMockedDependencyFactory" />
        public IMockedDependency<T> CreateDecoratedDependency<T>(T decoratee)
            where T : notnull
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{typeof(T).FullName}'");
            }

            var arrangements = new ArrangementCollection();
            var interceptor = InterceptorFactory.CreateInterceptorFor(MockBehavior.Partial, arrangements);
            var proxy = DynamicProxyFactory.CreateDecorator<T>(decoratee, interceptor);
            return new MockedDependency<T>(arrangements, interceptor, proxy);
        }

        /// <inheritdoc cref="IMockedDependencyFactory" />
        public IMockedDependency CreateMockedDependency(Type dependency, MockBehavior behavior)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }
            else if (!dependency.IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{dependency.FullName}'");
            }

            var arrangements = new ArrangementCollection();
            var interceptor = InterceptorFactory.CreateInterceptorFor(behavior, arrangements);
            var proxy = DynamicProxyFactory.CreateForInterface(dependency, interceptor);
            var mockedDependencyType = typeof(MockedDependency<>).MakeGenericType(dependency);
            var instance = Activator.CreateInstance(mockedDependencyType, new[] { arrangements, interceptor, proxy });
            if (instance is IMockedDependency mockedDependency)
            {
                return mockedDependency;
            }

            throw new InvalidOperationException("Invalid mocked dependency instance");
        }

        /// <inheritdoc cref="IMockedDependencyFactory" />
        public IMockedDependency<T> CreateMockedDependency<T>(MockBehavior behavior)
            where T : notnull
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{typeof(T).FullName}'");
            }

            var arrangements = new ArrangementCollection();
            var interceptor = InterceptorFactory.CreateInterceptorFor(behavior, arrangements);
            var proxy = DynamicProxyFactory.CreateForInterface<T>(interceptor);
            return new MockedDependency<T>(arrangements, interceptor, proxy);
        }

        #endregion
    }
}