namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Arrangements;
    using Interception;
    using LightInject;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="DynamicProxyFactory"/> type.
    /// </summary>
    public sealed class DynamicProxyFactoryTests
    {
        [Fact(DisplayName = "Create a new dynamic proxy that implements an interface at runtime")]
        public void CreateDynamicProxyForInterface()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();

            // When
            var proxy = proxyFactory.CreateForInterface<IFoo>(new LooseMockInterceptor(new ArrangementCollection()));

            // Then
            Assert.NotNull(proxy);
        }

        [Fact(DisplayName = "Create a new dynamic proxy decorator that implements an interface at runtime")]
        public void CreateDynamicProxyDecorator()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            IFoo decoratee = new Foo();

            // When
            var proxy = proxyFactory.CreateDecorator<IFoo>(
                decoratee,
                new LooseMockInterceptor(new ArrangementCollection()));

            // Then
            Assert.NotNull(proxy);
        }

        #region Test Domain

        private sealed class Foo : IFoo
        { }

        #endregion
    }
}