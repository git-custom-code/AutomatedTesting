namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Extensions;
using Interception;
using Interception.Async;
using System;
using System.Collections.Generic;
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
    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (value type) without parameters")]
    public async Task AsyncEnumerableValueTypeWithoutParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResults = new[] { 13, 42, 99 };
        var interceptor = new AsyncEnumerableInterceptor<int>(Array.Empty<int>(), false);
        var decoratee = new FooAsyncEnumerableValueTypeParameterless(expectedResults);
        var actualResults = new List<int>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableValueTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await foreach(var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (value type) without parameters (intercepted)")]
    public async Task AsyncEnumerableValueTypeWithoutParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResults = new[] { 13, 42, 99 };
        var interceptor = new AsyncEnumerableInterceptor<int>(expectedResults, true);
        var decoratee = new FooAsyncEnumerableValueTypeParameterless(Array.Empty<int>());
        var actualResults = new List<int>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableValueTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (value type) with single parameter")]
    public async Task AsyncEnumerableValueTypeWithSingleParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedValue = 17;
        var expectedResults = new[] { 13, 42, 99 };
        var interceptor = new AsyncEnumerableInterceptor<int>(Array.Empty<int>(), false);
        var decoratee = new FooAsyncEnumerableValueTypeParameter(expectedResults);
        var actualResults = new List<int>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (value type) with single parameter (intercepted)")]
    public async Task AsyncEnumerableValueTypeWithSingleParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedValue = 17;
        var expectedResults = new[] { 13, 42, 99 };
        var interceptor = new AsyncEnumerableInterceptor<int>(expectedResults, true);
        var decoratee = new FooAsyncEnumerableValueTypeParameter(Array.Empty<int>());
        var actualResults = new List<int>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (reference type) without parameters")]
    public async Task AsyncEnumerableReferenceTypeWithoutParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResults = new object?[] { new object(), null, new object() };
        var interceptor = new AsyncEnumerableInterceptor<object?>(Array.Empty<object?>(), false);
        var decoratee = new FooAsyncEnumerableReferenceTypeParameterless(expectedResults);
        var actualResults = new List<object?>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableReferenceTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (reference type) without parameters (intercepted)")]
    public async Task AsyncEnumerableReferenceTypeWithoutParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResults = new object?[] { new object(), null, new object() };
        var interceptor = new AsyncEnumerableInterceptor<object?>(expectedResults, true);
        var decoratee = new FooAsyncEnumerableReferenceTypeParameterless(Array.Empty<object?>());
        var actualResults = new List<object?>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableReferenceTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (reference type) with single parameter")]
    public async Task AsyncEnumerableReferenceTypeWithSingleParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedValue = 17;
        var expectedResults = new object?[] { new object(), null, new object() };
        var interceptor = new AsyncEnumerableInterceptor<object?>(Array.Empty<object?>(), false);
        var decoratee = new FooAsyncEnumerableReferenceTypeParameter(expectedResults);
        var actualResults = new List<object?>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: AsyncEnumerable (reference type) with single parameter (intercepted)")]
    public async Task AsyncEnumerableReferenceTypeWithSingleParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedValue = 17;
        var expectedResults = new object?[] { new object(), null, new object() };
        var interceptor = new AsyncEnumerableInterceptor<object?>(expectedResults, true);
        var decoratee = new FooAsyncEnumerableReferenceTypeParameter(Array.Empty<object?>());
        var actualResults = new List<object?>();


        // When
        var foo = proxyFactory.CreateDecorator<IFooAsyncEnumerableReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        await foreach (var result in task.ConfigureAwait(false))
        {
            actualResults.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, actualResults.Count);
        Assert.Equal(expectedResults, actualResults);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
    }

    #region Interceptor

    private sealed class AsyncEnumerableInterceptor<T> : IInterceptor
    {
        public AsyncEnumerableInterceptor(IEnumerable<T> result, bool wasIntercepted)
        {
            Result = result.ToAsyncEnumerable<T>();
            WasIntercepted = wasIntercepted;
        }

        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        private IAsyncEnumerable<T> Result { get; }

        private bool WasIntercepted { get; }

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IAsyncInvocation<IAsyncEnumerable<T>>>(out var asyncFeature))
            {
                asyncFeature.AsyncReturnValue = Result;
            }

            return WasIntercepted;
        }
    }

    #endregion
}
