namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Interception;
using Mocks.Core.Context;
using Mocks.Core.Data;
using Mocks.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="DecorateSetterEmitter"/> type.
/// </summary>
public sealed class DecorateSetterEmitterTests : IClassFixture<ProxyFactoryContext>
{
    #region Dependencies

    public DecorateSetterEmitterTests(ProxyFactoryContext context)
    {
        Context = context;
    }

    private ProxyFactoryContext Context { get; }

    #endregion

    [Theory(DisplayName = "DecorateSetterEmitter: Setter (value type)")]
    [ClassData(typeof(ValueTypeData))]
    public void PropertySetterValueType<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(false);
        var decoratee = new FooValueTypeSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeSetter<T>>(decoratee, interceptor);
        foo.Setter = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeSetter<T>.Setter));
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Setter (value type) (intercepted)")]
    [ClassData(typeof(ValueTypeData))]
    public void PropertySetterValueTypeIntercepted<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(true);
        var decoratee = new FooValueTypeSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeSetter<T>>(decoratee, interceptor);
        foo.Setter = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeSetter<T>.Setter));
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Indexed Setter (value type)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void PropertyIndexedSetterValueType<T>(T expectedValue, T expectedIndex)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(false);
        var decoratee = new FooValueTypeIndexedSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedSetter<T>>(decoratee, interceptor);
        foo[expectedIndex] = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedIndex, decoratee.Parameter);
        Assert.Equal(expectedValue, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedIndex);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Indexed Setter (value type) (intercepted)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void PropertyIndexedSetterValueTypeIntercepted<T>(T expectedValue, T expectedIndex)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(true);
        var decoratee = new FooValueTypeIndexedSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedSetter<T>>(decoratee, interceptor);
        foo[expectedIndex] = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(default, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedIndex);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Setter (reference type)")]
    [ClassData(typeof(ReferenceTypeData))]
    public void PropertySetterReferenceType<T>(T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(false);
        var decoratee = new FooReferenceTypeSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeSetter<T>>(decoratee, interceptor);
        foo.Setter = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeSetter<T>.Setter));
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Setter (reference type) (intercepted)")]
    [ClassData(typeof(ReferenceTypeData))]
    public void PropertySetterReferenceTypeIntercepted<T>(T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(true);
        var decoratee = new FooReferenceTypeSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeSetter<T>>(decoratee, interceptor);
        foo.Setter = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeSetter<T>.Setter));
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Indexed Setter (reference type)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void PropertyIndexedSetterReferenceType<T>(T? expectedValue, T? expectedIndex)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(false);
        var decoratee = new FooReferenceTypeIndexedSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedSetter<T>>(decoratee, interceptor);
        foo[expectedIndex] = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedIndex, decoratee.Parameter);
        Assert.Equal(expectedValue, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedIndex);
    }

    [Theory(DisplayName = "DecorateSetterEmitter: Indexed Setter (reference type) (intercepted)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void PropertyIndexedSetterReferenceTypeIntercepted<T>(T? expectedValue, T? expectedIndex)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new SetterInterceptor(true);
        var decoratee = new FooReferenceTypeIndexedSetter<T>();

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedSetter<T>>(decoratee, interceptor);
        foo[expectedIndex] = expectedValue;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(default, decoratee.Value);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedIndex);
    }

    #region Interceptor

    private sealed class SetterInterceptor : IInterceptor
    {
        public SetterInterceptor(bool wasIntercepted)
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
