namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Context;
using Core.Extensions;
using Interception;
using System.Collections.Generic;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="DecorateActionEmitter"/> type.
/// </summary>
public sealed partial class DecorateActionEmitterTests : IClassFixture<ProxyFactoryContext>
{
    #region Dependencies

    public DecorateActionEmitterTests(ProxyFactoryContext context)
    {
        Context = context;
    }

    private ProxyFactoryContext Context { get; }

    #endregion

    [Fact(DisplayName = "DecorateActionEmitter: Action without parameters")]
    public void ActionWithoutParameters()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor(false);
        var decoratee = new FooActionParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooActionParameterless>(decoratee, interceptor);
        foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionParameterless.MethodWithoutParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Fact(DisplayName = "DecorateActionEmitter: Action without parameters (intercepted)")]
    public void ActionWithoutParametersIntercepted()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor(true);
        var decoratee = new FooActionParameterless();

        // When
        var foo = proxyFactory.CreateDecorator<IFooActionParameterless>(decoratee, interceptor);
        foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionParameterless.MethodWithoutParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    #region Interceptor

    private sealed class ActionInterceptor : IInterceptor
    {
        public ActionInterceptor(bool wasIntercepted)
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
