namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Extensions;
using Interception.Async;
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
    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask without parameters")]
    public async Task ValueTaskWithoutParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var decoratee = new FooValueTaskParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask without parameters (intercepted)")]
    public async Task ValueTaskWithoutParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var decoratee = new FooValueTaskParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask (value type) with single parameter")]
    public async Task ValueTaskWithSingleValueTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var expectedValueType = 13;
        var decoratee = new FooValueTaskValueTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValueType, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask (value type) with single parameter (intercepted)")]
    public async Task ValueTaskWithSingleValueTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var expectedValueType = 13;
        var decoratee = new FooValueTaskValueTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask (reference type) with single parameter")]
    public async Task ValueTaskWithSingleReferenceTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var expectedReferenceType = typeof(int);
        var decoratee = new FooValueTaskReferenceTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedReferenceType, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: ValueTask (reference type) with single parameter (intercepted)")]
    public async Task ValueTaskWithSingleReferenceTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var expectedReferenceType = typeof(int);
        var decoratee = new FooValueTaskReferenceTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooValueTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.ValueTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }
}
