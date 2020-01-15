namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptActionEmitter"/> type.
    /// </summary>
    public sealed class InterceptActionEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an interface action")]
        public void EmitMethodImplementationForInterfaceAction()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ActionInterceptor();
            var expectedValueType = 0;
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAction>(interceptor);
            foo.Bar(expectedValueType, expectedReferenceType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as ActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAction.Bar), invocation?.Signature.Name);
        }

        #region Domain

        public interface IFooWithAction
        {
            void Bar(int @valueType, object @referenceType);
        }

        #endregion

        #region Mocks

        public sealed class ActionInterceptor : IInterceptor
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