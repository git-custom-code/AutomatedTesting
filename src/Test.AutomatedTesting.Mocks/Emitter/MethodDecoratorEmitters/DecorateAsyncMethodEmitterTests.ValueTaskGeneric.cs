namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Extensions;
    using Interception;
    using Interception.Async;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="DecorateAsyncMethodEmitter{T}"/> type.
    /// </summary>
    public sealed partial class DecorateAsyncMethodEmitterTests
    {
        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (value type) without parameters")]
        public async Task GenericValueTaskWithoutValueTypeParametersAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = 42;
            var interceptor = new AsyncGenericValueTaskInterceptor<int>(default, false);
            var decoratee = new FooGenericValueTaskValueTypeParameterless(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskValueTypeParameterless>(decoratee, interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (value type) without parameters (intercepted)")]
        public async Task GenericValueTaskWithoutValueTypeParametersInterceptedAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = 42;
            var interceptor = new AsyncGenericValueTaskInterceptor<int>(expectedResult, true);
            var decoratee = new FooGenericValueTaskValueTypeParameterless(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskValueTypeParameterless>(decoratee, interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (value type) with single parameter")]
        public async Task GenericValueTaskWithSingleValueTypeParameterAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = 42;
            var expectedValue = 13;
            var interceptor = new AsyncGenericValueTaskInterceptor<int>(default, false);
            var decoratee = new FooGenericValueTaskValueTypeParameter(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskValueTypeParameter>(decoratee, interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValue);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameter);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (value type) with single parameter (intercepted)")]
        public async Task GenericValueTaskWithSingleValueTypeParameterInterceptedAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = 42;
            var expectedValue = 13;
            var interceptor = new AsyncGenericValueTaskInterceptor<int>(expectedResult, true);
            var decoratee = new FooGenericValueTaskValueTypeParameter(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskValueTypeParameter>(decoratee, interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValue);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(default, decoratee.Parameter);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskValueTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (reference type) without parameters")]
        public async Task GenericValueTaskWithoutReferenceTypeParametersAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = new object();
            var interceptor = new AsyncGenericValueTaskInterceptor<object?>(default, false);
            var decoratee = new FooGenericValueTaskReferenceTypeParameterless(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskReferenceTypeParameterless>(decoratee, interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (reference type) without parameters (intercepted)")]
        public async Task GenericValueTaskWithoutReferenceTypeParametersInterceptedAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = new object();
            var interceptor = new AsyncGenericValueTaskInterceptor<object?>(expectedResult, true);
            var decoratee = new FooGenericValueTaskReferenceTypeParameterless(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskReferenceTypeParameterless>(decoratee, interceptor);
            var task = foo.MethodWithoutParameterAsync();
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveNoParameterIn();
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (reference type) with single parameter")]
        public async Task GenericValueTaskWithSingleReferenceTypeParameterAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = new object();
            var expectedValue = new object();
            var interceptor = new AsyncGenericValueTaskInterceptor<object?>(default, false);
            var decoratee = new FooGenericValueTaskReferenceTypeParameter(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskReferenceTypeParameter>(decoratee, interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValue);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameter);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
        }

        [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericValueTask (reference type) with single parameter (intercepted)")]
        public async Task GenericValueTaskWithSingleReferenceTypeParameterInterceptedAsync()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var expectedResult = new object();
            var expectedValue = new object();
            var interceptor = new AsyncGenericValueTaskInterceptor<object?>(expectedResult, true);
            var decoratee = new FooGenericValueTaskReferenceTypeParameter(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooGenericValueTaskReferenceTypeParameter>(decoratee, interceptor);
            var task = foo.MethodWithOneParameterAsync(expectedValue);
            var result = await task.ConfigureAwait(false);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(default, decoratee.Parameter);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooGenericValueTaskReferenceTypeParameter.MethodWithOneParameterAsync));
            invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericValueTask);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
        }

        #region Interceptor

        private sealed class AsyncGenericValueTaskInterceptor<T> : IInterceptor
        {
            public AsyncGenericValueTaskInterceptor([AllowNull] T value, bool wasIntercepted)
            {
                Value = value;
                WasIntercepted = wasIntercepted;
            }

            private bool WasIntercepted { get; }

            [AllowNull, MaybeNull]
            private T Value { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IAsyncInvocation<ValueTask<T>>>(out var asyncFeature))
                {
#nullable disable
                    asyncFeature.AsyncReturnValue = new ValueTask<T>(Value);
#nullable restore
                }

                return WasIntercepted;
            }
        }

        #endregion
    }
}