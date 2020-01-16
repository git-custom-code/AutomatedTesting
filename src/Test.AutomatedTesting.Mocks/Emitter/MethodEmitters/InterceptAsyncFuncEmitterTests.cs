namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncFuncEmitter"/> type.
    /// </summary>
    public sealed class InterceptAsyncFuncEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous interface func")]
        public async Task EmitMethodImplementationForAsynchronousInterfaceFuncAsync()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncFuncInterceptor();
            var expectedValueType = 0;
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncFunc>(interceptor);
            var result = await foo.BarAsync(expectedValueType, expectedReferenceType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as AsyncFuncInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncFunc.BarAsync), invocation?.Signature.Name);
            Assert.Equal(42.0, result);
        }

        #region Domain

        public interface IFooWithAsyncFunc
        {
            Task<double> BarAsync(int @valueType, object @referenceType);
        }

        #endregion

        #region Mocks

        public sealed class AsyncFuncInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                if (invocation is AsyncFuncInvocation asyncInvocation)
                {
                    asyncInvocation.ReturnValue = Task.FromResult<double>(42.0);
                }

                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}