namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

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
    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (value type) without parameters")]
    public async Task GenericTaskWithoutValueTypeParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = 42;
        var interceptor = new AsyncGenericTaskInterceptor<int>(default, false);
        var decoratee = new FooGenericTaskValueTypeParameterless(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskValueTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (value type) without parameters (intercepted)")]
    public async Task GenericTaskWithoutValueTypeParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = 42;
        var interceptor = new AsyncGenericTaskInterceptor<int>(expectedResult, true);
        var decoratee = new FooGenericTaskValueTypeParameterless(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskValueTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (value type) with single parameter")]
    public async Task GenericTaskWithSingleValueTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = 42;
        var expectedValue = 13;
        var interceptor = new AsyncGenericTaskInterceptor<int>(default, false);
        var decoratee = new FooGenericTaskValueTypeParameter(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (value type) with single parameter (intercepted)")]
    public async Task GenericTaskWithSingleValueTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = 42;
        var expectedValue = 13;
        var interceptor = new AsyncGenericTaskInterceptor<int>(expectedResult, true);
        var decoratee = new FooGenericTaskValueTypeParameter(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (reference type) without parameters")]
    public async Task GenericTaskWithoutReferenceTypeParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = new object();
        var interceptor = new AsyncGenericTaskInterceptor<object?>(default, false);
        var decoratee = new FooGenericTaskReferenceTypeParameterless(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskReferenceTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (reference type) without parameters (intercepted)")]
    public async Task GenericTaskWithoutReferenceTypeParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = new object();
        var interceptor = new AsyncGenericTaskInterceptor<object?>(expectedResult, true);
        var decoratee = new FooGenericTaskReferenceTypeParameterless(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskReferenceTypeParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (reference type) with single parameter")]
    public async Task GenericTaskWithSingleReferenceTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = new object();
        var expectedValue = new object();
        var interceptor = new AsyncGenericTaskInterceptor<object?>(default, false);
        var decoratee = new FooGenericTaskReferenceTypeParameter(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: GenericTask (reference type) with single parameter (intercepted)")]
    public async Task GenericTaskWithSingleReferenceTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var expectedResult = new object();
        var expectedValue = new object();
        var interceptor = new AsyncGenericTaskInterceptor<object?>(expectedResult, true);
        var decoratee = new FooGenericTaskReferenceTypeParameter(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooGenericTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValue);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedValue);
    }

    #region Interceptor

    private sealed class AsyncGenericTaskInterceptor<T> : IInterceptor
    {
        public AsyncGenericTaskInterceptor([AllowNull] T value, bool wasIntercepted)
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
            if (invocation.TryGetFeature<IAsyncInvocation<Task<T>>>(out var asyncFeature))
            {
#nullable disable
                asyncFeature.AsyncReturnValue = Task.FromResult(Value);
#nullable restore
            }

            return WasIntercepted;
        }
    }

    #endregion
}
