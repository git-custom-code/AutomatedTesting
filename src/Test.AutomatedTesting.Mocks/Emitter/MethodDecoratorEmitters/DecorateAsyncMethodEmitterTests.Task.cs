namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Context;
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
/// Automated tests for the <see cref="DecorateAsyncMethodEmitter{T}"/> type.
/// </summary>
public sealed partial class DecorateAsyncMethodEmitterTests : IClassFixture<ProxyFactoryContext>
{
    #region Dependencies

    public DecorateAsyncMethodEmitterTests(ProxyFactoryContext context)
    {
        Context = context;
    }

    private ProxyFactoryContext Context { get; }

    #endregion

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task without parameters")]
    public async Task TaskWithoutParametersAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var decoratee = new FooTaskParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task without parameters (intercepted)")]
    public async Task TaskWithoutParametersInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var decoratee = new FooTaskParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskParameterless>(decoratee, interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task (value type) with single parameter")]
    public async Task TaskWithSingleValueTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var expectedValueType = 13;
        var decoratee = new FooTaskValueTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValueType, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task (value type) with single parameter (intercepted)")]
    public async Task TaskWithSingleValueTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var expectedValueType = 13;
        var decoratee = new FooTaskValueTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskValueTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task (reference type) with single parameter")]
    public async Task TaskWithSingleReferenceTypeParameterAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(false);
        var expectedReferenceType = typeof(int);
        var decoratee = new FooTaskReferenceTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedReferenceType, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "DecorateAsyncMethodEmitter: Task (reference type) with single parameter (intercepted)")]
    public async Task TaskWithSingleReferenceTypeParameterInterceptedAsync()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new AsyncInterceptor(true);
        var expectedReferenceType = typeof(int);
        var decoratee = new FooTaskReferenceTypeParameter();

        // When
        var foo = proxyFactory.CreateDecorator<IFooTaskReferenceTypeParameter>(decoratee, interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    #region Interceptor

    private sealed class AsyncInterceptor : IInterceptor
    {
        public AsyncInterceptor(bool wasIntercepted)
        {
            WasIntercepted = wasIntercepted;
        }

        private bool WasIntercepted { get; }

        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            return WasIntercepted;
        }
    }

    #endregion
}
