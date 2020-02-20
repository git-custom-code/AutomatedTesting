namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptSetterEmitter"/> type.
    /// </summary>
    public sealed class InterceptSetterEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic property implementation for an interface property setter")]
        public void EmitImplementationForInterfacePropertySetter()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithSetter>(interceptor);
            foo.Bar = 42.0;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as SetterInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithSetter.Bar), invocation?.Signature.Name);
            Assert.Equal(42.0, invocation?.Value);
        }

        #region Domain

        public interface IFooWithSetter
        {
            double Bar { set; }
        }

        #endregion

        #region Mocks

        public sealed class SetterInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}