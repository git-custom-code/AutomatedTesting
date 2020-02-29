namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
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
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeProperties>(interceptor);
            foo.Setter = 42;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as SetterInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithValueTypeProperties.Setter), invocation?.PropertySignature.Name);
            Assert.Equal(42, invocation?.Value);
        }

        #region Mocks

        private sealed class SetterInterceptor : IInterceptor
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