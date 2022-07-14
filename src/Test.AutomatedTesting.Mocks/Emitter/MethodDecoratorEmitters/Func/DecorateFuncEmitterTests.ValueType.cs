namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Data;
using Core.Extensions;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="DecorateFuncEmitter{T}"/> type.
/// </summary>
public sealed partial class DecorateFuncEmitterTests
{
    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) without parameters")]
    [ClassData(typeof(ValueTypeData))]
    public void FuncValueTypeWithoutParameters<T>(T expectedResult)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(default, false);
        var decoratee = new FooFuncValueTypeParameterless<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterless<T>>(decoratee, interceptor);
        var result = foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterless<T>.MethodWithoutParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) without parameters (intercepted)")]
    [ClassData(typeof(ValueTypeData))]
    public void FuncValueTypeWithoutParametersIntercepted<T>(T expectedResult)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(default, true);
        var decoratee = new FooFuncValueTypeParameterless<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterless<T>>(decoratee, interceptor);
        var result = foo.MethodWithoutParameter();

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterless<T>.MethodWithoutParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single input parameter")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterIn<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(default, false);
        var decoratee = new FooFuncValueTypeParameterIn<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterIn<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(expectedValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterIn<T>.MethodWithOneParameter));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single input parameter (intercepted)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterInIntercepted<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(default, true);
        var decoratee = new FooFuncValueTypeParameterIn<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterIn<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(expectedValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Empty(decoratee.Parameters);
        Assert.Equal(default, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterIn<T>.MethodWithOneParameter));
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single ref parameter")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterRef<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult, false);
        var decoratee = new FooFuncValueTypeParameterRef<T>(expectedResult);
        var actualValue = expectedValue;

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterRef<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(ref actualValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
        Assert.Equal(expectedResult, result);
        Assert.Equal(default, actualValue);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterRef<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single ref parameter (intercepted)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterRefIntercepted<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult, true);
        var decoratee = new FooFuncValueTypeParameterRef<T>(expectedValue);
        var actualValue = expectedValue;

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterRef<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(ref actualValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Empty(decoratee.Parameters);
        Assert.Equal(expectedResult, result);
        Assert.Equal(expectedValue, actualValue);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterRef<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveParameterRefCountOf(1);
        invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
        invocation.ShouldHaveNoParameterOut();
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single out parameter")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterOut<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(default, false);
        var decoratee = new FooFuncValueTypeParameterOut<T>(expectedResult, expectedValue);
        
        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterOut<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(out var outValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, outValue);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterOut<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
    }

    [Theory(DisplayName = "DecorateFuncEmitter: Func (value type) with single out parameter (intercepted)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void FuncValueTypeWithSingleParameterOutIntercepted<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new FuncInterceptor<T>(expectedResult, true);
        var decoratee = new FooFuncValueTypeParameterOut<T>(default, expectedValue);

        // When
        var foo = proxyFactory.CreateDecorator<IFooFuncValueTypeParameterOut<T>>(decoratee, interceptor);
        var result = foo.MethodWithOneParameter(out var outValue);

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, outValue);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterOut<T>.MethodWithOneParameter));
        invocation.ShouldHaveNoParameterIn();
        invocation.ShouldHaveNoParameterRef();
        invocation.ShouldHaveParameterOutCountOf(1);
        invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
    }
}
