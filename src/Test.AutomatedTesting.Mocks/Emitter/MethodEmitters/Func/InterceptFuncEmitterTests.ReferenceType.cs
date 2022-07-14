namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Data;
using Core.Extensions;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="InterceptFuncEmitter{T}"/> type.
/// </summary>
public sealed partial class InterceptFuncEmitterTests
{
    [Theory(DisplayName = "MethodEmitter: Func (reference type) without parameters")]
    [ClassData(typeof(ReferenceTypeData))]
    public void FuncReferenceTypeWithoutParameters<T>(T? expectedResult)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeParameterless<T>>(interceptor);
        var result = foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterless<T>.MethodWithoutParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with single input parameter")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSingleParameterIn<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeParameterIn<T>>(interceptor);
        var result = foo.MethodWithOneParameter(expectedValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterIn<T>.MethodWithOneParameter));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with single ref parameter")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSingleParameterRef<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeParameterRef<T>>(interceptor);
        var result = foo.MethodWithOneParameter(ref expectedValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterIn<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with replaced single ref parameter")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithReplacedSingleParameterRef<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
        var replacedRefValue = expectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeParameterRef<T>>(interceptor);
        var result = foo.MethodWithOneParameter(ref replacedRefValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterRef<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        Assert.Equal(default, replacedRefValue);
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with single out parameter")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSingleParameterOut<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeParameterOut<T>>(interceptor);
        var result = foo.MethodWithOneParameter(out var outValue);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterOut<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        Assert.Equal(expectedValue, outValue);
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded method (first overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithFirstOverloadedMethod<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsIn<T>>(interceptor);
        var result = foo.MethodWithOverload(expectedValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded method (second overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSecondOverloadedMethod<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsIn<T>>(interceptor);
        var result = foo.MethodWithOverload(expectedValue, expectedValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveParameterIn("second", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded method (first overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithFirstOverloadedMethodWithReplacedRef<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
        var replacedRefValue = expectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsRef<T>>(interceptor);
        var result = foo.MethodWithOverload(ref replacedRefValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsRef<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        Assert.Equal(default, replacedRefValue);
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded method (second overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSecondOverloadedMethodWithReplacedRef<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
        var replacedFirstRefValue = expectedValue;
        var replacedSecondRefValue = expectedValue;

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsRef<T>>(interceptor);
        var result = foo.MethodWithOverload(ref replacedFirstRefValue, ref replacedSecondRefValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsRef<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(2);
        invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
        invocation.ShouldHaveParameterRef("second", typeof(T), default(T));
        Assert.Equal(default, replacedFirstRefValue);
        Assert.Equal(default, replacedSecondRefValue);
        invocation.ShouldHaveNoParameterOut();
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded out parameter method (first overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithFirstOverloadedOutParameterMethod<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsOut<T>>(interceptor);
        var result = foo.MethodWithOverload(out var outValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsOut<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        Assert.Equal(expectedValue, outValue);
        Assert.Equal(expectedResult, result);
    }

    [Theory(DisplayName = "MethodEmitter: Func (reference type) with overloaded out parameter method (second overload)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void FuncReferenceTypeWithSecondOverloadedOutParameterMethod<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

        // When
        var foo = proxyFactory.CreateForInterface<IFooFuncReferenceTypeOverloadsOut<T>>(interceptor);
        var result = foo.MethodWithOverload(out var firstOutValue, out var secondOutValue);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeOverloadsIn<T>.MethodWithOverload));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(2);
        invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
        invocation.ShouldHaveParameterOut("second", typeof(T), expectedValue);
        Assert.Equal(expectedValue, firstOutValue);
        Assert.Equal(expectedValue, secondOutValue);
        Assert.Equal(expectedResult, result);
    }
}
