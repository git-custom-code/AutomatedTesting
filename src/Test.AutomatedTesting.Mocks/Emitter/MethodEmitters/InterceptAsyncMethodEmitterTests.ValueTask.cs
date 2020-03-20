namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Extensions;
    using Interception.Async;
    using System.Linq;
    using System.Threading.Tasks;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncMethodEmitter{T}"/> type.
    /// </summary>
    public sealed partial class InterceptAsyncMethodEmitterTests
    {
        [Fact(DisplayName = "MethodEmitter: ValueTask without parameters")]
        public async Task ValueTaskWithoutParametersAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTaskParameterless>(interceptor);
            var task = foo.MethodWithoutParameterAsync();
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
            invocation.ShouldHaveNoParameterIn();
        }
    }
}