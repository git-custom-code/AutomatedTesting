namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncActionEmitter"/> type.
    /// </summary>
    public sealed class InterceptAsyncActionEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous value type interface action")]
        public async Task EmitMethodImplementationForAsynchronousValueTypeInterfaceActionAsync()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncActionInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncValueTypeAction>(interceptor);
            await foo.MethodWithOneParameterAsync(expectedValueType).ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as AsyncActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncValueTypeAction.MethodWithOneParameterAsync), invocation?.Signature.Name);
            Assert.Single(invocation?.InputParameter);
            Assert.Equal(typeof(int), invocation?.InputParameter?.First().type);
            Assert.Equal(expectedValueType, invocation?.InputParameter?.First().value);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous reference type interface action")]
        public async Task EmitMethodImplementationForAsynchronousReferenceTypeInterfaceActionAsync()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncActionInterceptor();
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncReferenceTypeAction>(interceptor);
            await foo.MethodWithOneParameterAsync(expectedReferenceType).ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as AsyncActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncReferenceTypeAction.MethodWithOneParameterAsync), invocation?.Signature.Name);
            Assert.Single(invocation?.InputParameter);
            Assert.Equal(typeof(object), invocation?.InputParameter?.First().type);
            Assert.Equal(expectedReferenceType, invocation?.InputParameter?.First().value);
        }

        #region Mocks

        private sealed class AsyncActionInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                if (invocation is AsyncActionInvocation asyncInvocation)
                {
                    asyncInvocation.ReturnValue = Task.CompletedTask;
                }

                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}