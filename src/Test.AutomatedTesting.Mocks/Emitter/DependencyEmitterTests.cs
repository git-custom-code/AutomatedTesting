namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Arrangements;
    using Interception;
    using LightInject;
    using System.Reflection;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="DependencyEmitter"/> type.
    /// </summary>
    public sealed class DependencyEmitterTests
    {
        [Fact(DisplayName = "DependencyEmitter: Dynamic type with interceptor")]
        public void EmitDynamicTypeWithInterceptorDependency()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var expectedInterceptor = new LooseMockInterceptor(new ArrangementCollection());

            // When
            var instance = proxyFactory.CreateForInterface<IFoo>(expectedInterceptor);

            // Then
            Assert.NotNull(instance);
            var interceptorField = instance?.GetType().GetField("_interceptor", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(interceptorField);
            var actualInterceptor = interceptorField?.GetValue(instance);
            Assert.NotNull(actualInterceptor);
            Assert.Equal(expectedInterceptor, actualInterceptor);
        }

        [Fact(DisplayName = "DependencyEmitter: Dynamic type with decoratee and interceptor")]
        public void EmitDynamicDecoratorTypeWithDecorateeAndInterceptorDependency()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var expectedInterceptor = new LooseMockInterceptor(new ArrangementCollection());
            var expectedDecoratee = new Foo();

            // When
            var instance = proxyFactory.CreateDecorator<IFoo>(expectedDecoratee, expectedInterceptor);

            // Then
            Assert.NotNull(instance);

            var decorateeField = instance?.GetType().GetField("_decoratee", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(decorateeField);
            var actualDecoratee = decorateeField?.GetValue(instance);
            Assert.NotNull(actualDecoratee);
            Assert.Equal(expectedDecoratee, actualDecoratee);

            var interceptorField = instance?.GetType().GetField("_interceptor", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(interceptorField);
            var actualInterceptor = interceptorField?.GetValue(instance);
            Assert.NotNull(actualInterceptor);
            Assert.Equal(expectedInterceptor, actualInterceptor);
        }

        #region Test Domain

        private sealed class Foo : IFoo
        { }

        #endregion
    }
}