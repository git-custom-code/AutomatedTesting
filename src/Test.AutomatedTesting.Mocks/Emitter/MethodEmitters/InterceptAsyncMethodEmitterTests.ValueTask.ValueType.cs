namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Interception.Async;
    using Mocks.Tests.Extensions;
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
        [Fact(DisplayName = "MethodEmitter: ValueTask (value type) with single parameter")]
        public async Task ValueTaskWithSingleValueTypeParameterAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTaskValueTypeParameter>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: ValueTask (value type) with overloaded method (first overload)")]
        public async Task ValueTaskWithFirstOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(expectedValueType);
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: ValueTask (value type) with overloaded method (second overload)")]
        public async Task ValueTaskWithSecondOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncInterceptor();
            var firstExpectedValueType = 13;
            var secondExpectedValueType = 42.0;

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(firstExpectedValueType, secondExpectedValueType);
            await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(int), firstExpectedValueType);
            invocation.ShouldHaveParameterIn("second", typeof(double), secondExpectedValueType);
        }
    }
}