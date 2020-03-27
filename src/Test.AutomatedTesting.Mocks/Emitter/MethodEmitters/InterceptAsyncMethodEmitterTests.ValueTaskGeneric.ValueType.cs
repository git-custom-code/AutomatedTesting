namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Extensions;
    using Interception;
    using Interception.Async;
    using System.Collections.Generic;
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
        [Fact(DisplayName = "MethodEmitter: GenericValueTask (value type) without parameters")]
        public async Task GenericValueTaskWithoutValueTypeParametersAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeValueTaskInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskValueTypeParameterless>(interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (value type) with single parameter")]
        public async Task GenericValueTaskWithSingleValueTypeParameterAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeValueTaskInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskValueTypeParameter>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (value type) with overloaded method (first overload)")]
        public async Task GenericValueTaskWithFirstOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeValueTaskInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(expectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (value type) with overloaded method (second overload)")]
        public async Task GenericValueTaskWithSecondOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeValueTaskInterceptor();
            var firstExpectedValueType = 13;
            var secondExpectedValueType = 42.0;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(firstExpectedValueType, secondExpectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(int), firstExpectedValueType);
            invocation.ShouldHaveParameterIn("second", typeof(double), secondExpectedValueType);
        }

        #region Interceptor

        private sealed class AsyncGenericValueTypeValueTaskInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IAsyncInvocation<ValueTask<int>>>(out var asyncFeature))
                {
                    asyncFeature.AsyncReturnValue = new ValueTask<int>(42);
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}