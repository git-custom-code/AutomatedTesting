namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System;
    using System.Reflection;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="DependencyEmitter"/> type.
    /// </summary>
    public sealed class DependencyEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic type with an interceptor dependency")]
        public void EmitDynamicTypeWithInterceptorDependency()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var expectedInterceptor = new LooseMockInterceptor();

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

        #region Domain

        public interface IFoo
        { }

        #endregion
    }
}