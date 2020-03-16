namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Async;
    using Interception.Parameters;
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
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncActionInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncValueTypeAction>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as Invocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncValueTypeAction.MethodWithOneParameterAsync), invocation?.Signature.Name);
            var inputParameter = invocation?.GetFeature<IParameterIn>();
            Assert.NotNull(inputParameter);
            Assert.Single(inputParameter?.InputParameterCollection);
            var parameter = inputParameter?.InputParameterCollection?.FirstOrDefault();
            Assert.Equal("first", parameter?.Name);
            Assert.Equal(typeof(int), parameter?.Type);
            Assert.Equal(expectedValueType, parameter?.Value);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous reference type interface action")]
        public async Task EmitMethodImplementationForAsynchronousReferenceTypeInterfaceActionAsync()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncActionInterceptor();
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncReferenceTypeAction>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as Invocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncReferenceTypeAction.MethodWithOneParameterAsync), invocation?.Signature.Name);
            var inputParameter = invocation?.GetFeature<IParameterIn>();
            Assert.NotNull(inputParameter);
            Assert.Single(inputParameter?.InputParameterCollection);
            var parameter = inputParameter?.InputParameterCollection?.FirstOrDefault();
            Assert.Equal("first", parameter?.Name);
            Assert.Equal(typeof(object), parameter?.Type);
            Assert.Equal(expectedReferenceType, parameter?.Value);
        }

        #region Mocks

        private sealed class AsyncActionInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                if (invocation.TryGetFeature<IAsyncInvocation<Task>>(out var asyncInvocation))
                {
                    asyncInvocation.ReturnValue = Task.CompletedTask;
                }

                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}