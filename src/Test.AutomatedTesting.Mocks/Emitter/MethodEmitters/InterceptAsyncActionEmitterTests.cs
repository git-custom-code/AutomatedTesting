namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncActionEmitter"/> type.
    /// </summary>
    public sealed class InterceptAsyncActionEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an asynchronous interface action")]
        public async Task EmitMethodImplementationForAsynchronousInterfaceActionAsync()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new AsyncActionInterceptor();
            var expectedValueType = 0;
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithAsyncAction>(interceptor);
            await foo.BarAsync(expectedValueType, expectedReferenceType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as AsyncActionInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithAsyncAction.BarAsync), invocation?.Signature.Name);
        }

        #region Domain

        public interface IFooWithAsyncAction
        {
            Task BarAsync(int @valueType, object @referenceType);
        }

        #endregion

        #region Mocks

        public sealed class AsyncActionInterceptor : IInterceptor
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