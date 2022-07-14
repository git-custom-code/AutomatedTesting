namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Data;
using Core.Extensions;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="InterceptActionEmitter"/> type.
/// </summary>
public sealed partial class InterceptActionEmitterTests
{
    [Theory(DisplayName = "MethodEmitter: Action (value type) with single input parameter")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithSingleParameterIn<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeParameterIn<T>>(interceptor);
        foo.MethodWithOneParameter(expectedValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterIn<T>.MethodWithOneParameter));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with single ref parameter")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithSingleParameterRef<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeParameterRef<T>>(interceptor);
        foo.MethodWithOneParameter(ref expectedValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterRef<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with replaced single ref parameter")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithReplacedSingleParameterRef<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor();
        var replacedRefValue = expectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeParameterRef<T>>(interceptor);
        foo.MethodWithOneParameter(ref replacedRefValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterRef<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        Assert.Equal(default, replacedRefValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with single out parameter")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithSingleParameterOut<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeParameterOut<T>>(interceptor);
        foo.MethodWithOneParameter(out var outValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterOut<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        Assert.Equal(expectedValue, outValue);
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded method (first overload)")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithFirstOverloadedMethod<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsIn<T>>(interceptor);
        foo.MethodWithOverload(expectedValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded method (second overload)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void ActionValueTypeWithSecondOverloadedMethod<T>(T firstExpectedValue, T secondExpectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ActionInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsIn<T>>(interceptor);
        foo.MethodWithOverload(firstExpectedValue, secondExpectedValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(T), firstExpectedValue);
        invocation.ShouldHaveParameterIn("second", typeof(T), secondExpectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded method (first overload)")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithFirstOverloadedMethodWithReplacedRef<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor();
        var replacedRefValue = expectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsRef<T>>(interceptor);
        foo.MethodWithOverload(ref replacedRefValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsRef<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        Assert.Equal(default, replacedRefValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded method (second overload)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void ActionValueTypeWithSecondOverloadedMethodWithReplacedRef<T>(T firstExpectedValue, T secondExpectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor();
        var replacedFirstRefValue = firstExpectedValue;
        var replacedSecondRefValue = secondExpectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsRef<T>>(interceptor);
        foo.MethodWithOverload(ref replacedFirstRefValue, ref replacedSecondRefValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsRef<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(2);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        invocation.ShouldHaveParameterRef("second", typeof(T), default(T));
        Assert.Equal(default, replacedFirstRefValue);
        Assert.Equal(default, replacedSecondRefValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded out parameter method (first overload)")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithFirstOverloadedMethodOut<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsOut<T>>(interceptor);
        foo.MethodWithOverload(out var outValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsOut<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        Assert.Equal(expectedValue, outValue);
    }

    [Theory(DisplayName = "MethodEmitter: Action (value type) with overloaded out parameter method (second overload)")]
    [ClassData(typeof(ValueTypeData))]
    public void ActionValueTypeWithSecondOverloadedMethodOut<T>(T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooActionValueTypeOverloadsOut<T>>(interceptor);
        foo.MethodWithOverload(out var firstOutValue, out var secondOutValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(2);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        invocation.ShouldHaveParameterOut("second", typeof(T), expectedValue);
        Assert.Equal(expectedValue, firstOutValue);
        Assert.Equal(expectedValue, secondOutValue);
    }
}
