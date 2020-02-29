namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptActionEmitter"/> type.
    /// </summary>
    public sealed class InterceptActionEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for a value type interface action")]
        public void EmitMethodImplementationForValueTypeInterfaceAction()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ActionInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeAction>(interceptor);
            foo.MethodWithOneParameter(expectedValueType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as ActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithValueTypeAction.MethodWithOneParameter), invocation?.Signature.Name);
            Assert.Single(invocation?.InputParameter);
            Assert.Equal(typeof(int), invocation?.InputParameter?.First().type);
            Assert.Equal(expectedValueType, invocation?.InputParameter?.First().value);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for a reference type interface action")]
        public void EmitMethodImplementationForReferenceTypeInterfaceAction()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ActionInterceptor();
            var expectedReferencType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeAction>(interceptor);
            foo.MethodWithOneParameter(expectedReferencType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as ActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithReferenceTypeAction.MethodWithOneParameter), invocation?.Signature.Name);
            Assert.Single(invocation?.InputParameter);
            Assert.Equal(typeof(object), invocation?.InputParameter?.First().type);
            Assert.Equal(expectedReferencType, invocation?.InputParameter?.First().value);
        }

        #region Mocks

        private sealed class ActionInterceptor : IInterceptor
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