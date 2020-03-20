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
        [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (value type) without parameters")]
        public async Task AsyncEnumerableValueTypeWithoutParametersAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncEnumerableValueTypeInterceptor();
            var results = new List<int>();

            // When
            var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableValueTypeParameterless>(interceptor);
            var task = foo.MethodWithoutParameterAsync();
            await foreach(var result in task.ConfigureAwait(false))
            {
                results.Add(result);
            }

            // Then
            Assert.NotNull(foo);
            Assert.Equal(3, results.Count);
            Assert.Equal(new[] { 99, 99, 99 }, results);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (value type) with single parameter")]
        public async Task AsyncEnumerableValueTypeWithSingleParameterAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncEnumerableValueTypeInterceptor();
            var expectedValueType = 13;
            var results = new List<int>();

            // When
            var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableValueTypeParameter>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            await foreach (var result in task.ConfigureAwait(false))
            {
                results.Add(result);
            }

            // Then
            Assert.NotNull(foo);
            Assert.Equal(3, results.Count);
            Assert.Equal(new[] { 99, 99, 99 }, results);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (value type) with overloaded method (first overload)")]
        public async Task AsyncEnumerableValueTypeWithFirstOverloadedMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncEnumerableValueTypeInterceptor();
            var expectedValueType = 13;
            var results = new List<int>();
            
            // When
            var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(expectedValueType);
            await foreach (var result in task.ConfigureAwait(false))
            {
                results.Add(result);
            }

            // Then
            Assert.NotNull(foo);
            Assert.Equal(3, results.Count);
            Assert.Equal(new[] { 99, 99, 99 }, results);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (value type) with overloaded method (second overload)")]
        public async Task AsyncEnumerableValueTypeWithSecondOverloadedMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncEnumerableValueTypeInterceptor();
            var firstExpectedValueType = 13;
            var secondExpectedValueType = 42.0;
            var results = new List<int>();

            // When
            var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(firstExpectedValueType, secondExpectedValueType);
            await foreach (var result in task.ConfigureAwait(false))
            {
                results.Add(result);
            }

            // Then
            Assert.NotNull(foo);
            Assert.Equal(3, results.Count);
            Assert.Equal(new[] { 99, 99, 99 }, results);
           
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(int), firstExpectedValueType);
            invocation.ShouldHaveParameterIn("second", typeof(double), secondExpectedValueType);
        }

        #region Interceptor

        private sealed class AsyncEnumerableValueTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IAsyncInvocation<IAsyncEnumerable<int>>>(out var asyncFeature))
                {
                    asyncFeature.AsyncReturnValue = AsyncEnumerable.Create(_ => new AsyncEnumeratorValueType());
                }
            }
        }

        private sealed class AsyncEnumeratorValueType : IAsyncEnumerator<int>
        {
            public int Current { get { return 99; } }

            private uint ElementCount { get; } = 3;

            private uint CurrentIndex { get; set; } = 0;

            public ValueTask DisposeAsync()
            {
                return default;
            }

            public ValueTask<bool> MoveNextAsync()
            {
                if (CurrentIndex < ElementCount)
                {
                    ++CurrentIndex;
                    return new ValueTask<bool>(true);
                }

                return new ValueTask<bool>(false);
            }
        }

        #endregion
    }
}