namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Interception;
    using Interception.Async;
    using Interception.Parameters;
    using Mocks.Tests.Extensions;
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
        [Fact(DisplayName = "MethodEmitter: GenericTask (value type) without parameters")]
        public async Task GenericTaskWithoutValueTypeParametersAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeTaskInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericTaskValueTypeParameterless>(interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "MethodEmitter: GenericTask (value type) with single parameter")]
        public async Task GenericTaskWithSingleValueTypeParameterAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeTaskInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericTaskValueTypeParameter>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericTask (value type) with overloaded method (first overload)")]
        public async Task GenericTaskWithFirstOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeTaskInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(expectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericTask (value type) with overloaded method (second overload)")]
        public async Task GenericTaskWithSecondOverloadedValueTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericValueTypeTaskInterceptor();
            var firstExpectedValueType = 13;
            var secondExpectedValueType = 42.0;

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericTaskValueTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(firstExpectedValueType, secondExpectedValueType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(42, result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(int), firstExpectedValueType);
            invocation.ShouldHaveParameterIn("second", typeof(double), secondExpectedValueType);
        }

        #region Interceptor

        private sealed class AsyncGenericValueTypeTaskInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IAsyncInvocation<Task<int>>>(out var asyncFeature))
                {
                    asyncFeature.AsyncReturnValue = Task.FromResult(42);
                }
            }
        }

        #endregion
    }
}