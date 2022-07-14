namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Context;
using Core.Extensions;
using Interception;
using Interception.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="InterceptActionEmitter"/> type.
/// </summary>
public sealed partial class InterceptActionEmitterTests : IClassFixture<ProxyFactoryContext>
{
    #region Dependencies

    public InterceptActionEmitterTests(ProxyFactoryContext context)
    {
        Context = context;
    }

    private ProxyFactoryContext Context { get; }

    #endregion

    [Fact(DisplayName = "MethodEmitter: Action without parameters")]
    public void ActionWithoutParameters()
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionParameterless>(interceptor);
        foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);

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
        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            return true;
        }
    }

    private sealed class ReplaceRefParameterInterceptor : IInterceptor
    {
        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IParameterRef>(out var parameterRef))
            {
                foreach (var parameter in parameterRef.RefParameterCollection)
                {
                    if (parameter.Type.IsValueType)
                    {
                        parameter.Value = Activator.CreateInstance(parameter.Type);
                    }
                    else
                    {
                        parameter.Value = null;
                    }
                }

                return true;
            }

            return false;
        }
    }

    private sealed class OutParameterInterceptor<T> : IInterceptor
    {
        public OutParameterInterceptor([AllowNull] T value)
        {
            Value = value;
        }

        [AllowNull, MaybeNull]
        private T Value { get; }

        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IParameterOut>(out var parameterOut))
            {
                foreach (var parameter in parameterOut.OutParameterCollection)
                {
                    parameter.Value = Value;
                }

                return true;
            }

            return false;
        }
    }

    #endregion
}
