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
            iocContainer.RegisterAssembly(typeof(IDependencyEmitter).Assembly);
            var asmEmitter = iocContainer.GetInstance<IAssemblyEmitter>();
            var expectedInterceptor = new LooseMockInterceptor();

            // When
            var emitter = asmEmitter.EmitType("My.Namespace.MyType");
            emitter.ImplementInterface<IFoo>();
            var type = emitter.ToType();
            var instance = Activator.CreateInstance(type, new[] { expectedInterceptor });

            // Then
            Assert.NotNull(instance);
            Assert.IsAssignableFrom<IFoo>(instance);
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