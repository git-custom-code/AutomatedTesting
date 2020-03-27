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
        [Fact(DisplayName = "MethodEmitter: GenericValueTask (reference type) without parameters")]
        public async Task GenericValueTaskWithoutReferenceTypeParametersAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericReferenceTypeValueTaskInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskReferenceTypeParameterless>(interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal("foo", result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (reference type) with single parameter")]
        public async Task GenericValueTaskWithSingleReferenceTypeParameterAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericReferenceTypeValueTaskInterceptor();
            var expectedReferenceType = typeof(int);

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskReferenceTypeParameter>(interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal("foo", result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (reference type) with overloaded method (first overload)")]
        public async Task GenericValueTaskWithFirstOverloadedReferenceTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericReferenceTypeValueTaskInterceptor();
            var expectedReferenceType = typeof(int);

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskReferenceTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(expectedReferenceType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal("foo", result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
        }

        [Fact(DisplayName = "MethodEmitter: GenericValueTask (reference type) with overloaded method (second overload)")]
        public async Task GenericValueTaskWithSecondOverloadedReferenceTypeMethodAsync()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new AsyncGenericReferenceTypeValueTaskInterceptor();
            var firstExpectedReferenceType = typeof(int);
            var secondExpectedReferenceType = typeof(string);

            // When
            var foo = proxyFactory.CreateForInterface<IFooGenericValueTaskReferenceTypeOverloads>(interceptor);
            var task = foo.MethodWithOverloadAsync(firstExpectedReferenceType, secondExpectedReferenceType);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal("foo", result);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeOverloads.MethodWithOverloadAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(object), firstExpectedReferenceType);
            invocation.ShouldHaveParameterIn("second", typeof(object), secondExpectedReferenceType);
        }

        #region Interceptor

        private sealed class AsyncGenericReferenceTypeValueTaskInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IAsyncInvocation<ValueTask<object?>>>(out var asyncFeature))
                {
                    asyncFeature.AsyncReturnValue = new ValueTask<object?>("foo");
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}