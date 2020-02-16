namespace CustomCode.AutomatedTesting.Mocks.Dependencies.Tests
{
    using LightInject;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="MockedDependencyFactory"/> type.
    /// </summary>
    public sealed class MockedDependencyFactoryTests
    {
        [Fact(DisplayName = "Create a new generic mock for an interface")]
        public void CreateMockedDependencyFromInterface()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IMockedDependencyFactory).Assembly);
            var factory = iocContainer.GetInstance<IMockedDependencyFactory>();

            // When
            var mock = factory.CreateMockedDependency<IFoo>(MockBehavior.Loose);

            // Then
            Assert.NotNull(mock);
            Assert.NotNull(mock.Arrangements);
            Assert.NotNull(mock.Instance);
            Assert.NotNull(mock.Interceptor);
            Assert.IsAssignableFrom<IFoo>(mock.Instance);
        }

        [Fact(DisplayName = "Create a new mock for an interface type")]
        public void CreateMockedDependencyFromInterfaceType()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IMockedDependencyFactory).Assembly);
            var factory = iocContainer.GetInstance<IMockedDependencyFactory>();

            // When
            var mock = factory.CreateMockedDependency(typeof(IFoo), MockBehavior.Loose);

            // Then
            Assert.NotNull(mock);
            Assert.NotNull(mock.Arrangements);
            Assert.NotNull(mock.Instance);
            Assert.NotNull(mock.Interceptor);
            Assert.IsAssignableFrom<IFoo>(mock.Instance);
        }

        #region Domain

        public interface IFoo
        { }

        #endregion
    }
}