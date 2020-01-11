namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using CustomCode.AutomatedTesting.Mocks.Interception;
    using LightInject;
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
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();

            // When
            var proxy = proxyFactory.CreateForInterface<IFoo>(new LooseMockInterceptor());

            // Then
            Assert.NotNull(proxy);
        }

        #region Domain

        public interface IFoo
        { }

        #endregion
    }
}