namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Async;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncFuncEmitter"/> type.
    /// </summary>
    public sealed class InterceptAsyncFuncEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous value type interface func")]
        public async Task EmitMethodImplementationForAsynchronousValueTypeInterfaceFuncAsync()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncFuncInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncValueTypeFunc>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as Invocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncValueTypeFunc.MethodWithOneParameterAsync), invocation?.Signature.Name);
            Assert.Equal(42, result);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous reference type interface func")]
        public async Task EmitMethodImplementationForAsynchronousReferenceTypeInterfaceFuncAsync()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncFuncInterceptor();
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncReferenceTypeFunc>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as Invocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithOneParameterAsync), invocation?.Signature.Name);
            Assert.Equal("foo", result);
        }

        #region Mocks

        private sealed class AsyncFuncInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                if (invocation.TryGetFeature<IAsyncInvocation<Task<int>>>(out var asyncValueType))
                {
                    asyncValueType.ReturnValue = Task.FromResult<int>(42);
                }
                else if (invocation.TryGetFeature<IAsyncInvocation<Task<object?>>>(out var asyncReferenceType))
                {
                    asyncReferenceType.ReturnValue = Task.FromResult<object?>("foo");
                }

                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}