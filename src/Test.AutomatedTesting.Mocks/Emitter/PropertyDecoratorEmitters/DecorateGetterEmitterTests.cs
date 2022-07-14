namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Interception;
using Interception.ReturnValue;
using Mocks.Core.Context;
using Mocks.Core.Data;
using Mocks.Core.Extensions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="DecorateGetterEmitter{T}"/> type.
/// </summary>
public sealed class DecorateGetterEmitterTests : IClassFixture<ProxyFactoryContext>
{
    #region Dependencies

    public DecorateGetterEmitterTests(ProxyFactoryContext context)
    {
        Context = context;
    }

    private ProxyFactoryContext Context { get; }

    #endregion

    [Theory(DisplayName = "DecorateGetterEmitter: Getter (value type)")]
    [ClassData(typeof(ValueTypeData))]
    public void PropertyGetterValueType<T>(T expectedResult)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(default, false);
        var decoratee = new FooValueTypeGetter<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeGetter<T>>(decoratee, interceptor);
        var result = foo.Getter;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeGetter<T>.Getter));
        invocation.ShouldHaveReturnValue(default(T));
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Getter (value type) (intercepted)")]
    [ClassData(typeof(ValueTypeData))]
    public void PropertyGetterValueTypeIntercepted<T>(T expectedResult)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(expectedResult, true);
        var decoratee = new FooValueTypeGetter<T>(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeGetter<T>>(decoratee, interceptor);
        var result = foo.Getter;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeGetter<T>.Getter));
        invocation.ShouldHaveReturnValue(expectedResult);
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Indexed Getter (value type)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void PropertyIndexedGetterValueType<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(default, false);
        var decoratee = new FooValueTypeIndexedGetter<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedGetter<T>>(decoratee, interceptor);
        var result = foo[expectedValue];

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveReturnValue(default(T));
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Indexed Getter (value type) (intercepted)")]
    [ClassData(typeof(TwoParameterValueTypeData))]
    public void PropertyIndexedGetterValueTypeIntercepted<T>(T expectedResult, T expectedValue)
        where T : struct
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(expectedResult, true);
        var decoratee = new FooValueTypeIndexedGetter<T>(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedGetter<T>>(decoratee, interceptor);
        var result = foo[expectedValue];

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveReturnValue(expectedResult);
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Getter (reference type)")]
    [ClassData(typeof(ReferenceTypeData))]
    public void PropertyGetterReferenceType<T>(T? expectedResult)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(default, false);
        var decoratee = new FooReferenceTypeGetter<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeGetter<T>>(decoratee, interceptor);
        var result = foo.Getter;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeGetter<T>.Getter));
        invocation.ShouldHaveReturnValue(default(T));
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Getter (reference type) (intercepted)")]
    [ClassData(typeof(ReferenceTypeData))]
    public void PropertyGetterReferenceTypeIntercepted<T>(T? expectedResult)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(expectedResult, true);
        var decoratee = new FooReferenceTypeGetter<T>(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeGetter<T>>(decoratee, interceptor);
        var result = foo.Getter;

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeGetter<T>.Getter));
        invocation.ShouldHaveReturnValue(expectedResult);
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Indexed Getter (reference type)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void PropertyIndexedGetterReferenceType<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(default, false);
        var decoratee = new FooReferenceTypeIndexedGetter<T>(expectedResult);

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedGetter<T>>(decoratee, interceptor);
        var result = foo[expectedValue];

        // Then
        Assert.NotNull(foo);
        Assert.Equal(1u, decoratee.CallCount);
        Assert.Equal(expectedValue, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveReturnValue(default(T));
    }

    [Theory(DisplayName = "DecorateGetterEmitter: Indexed Getter (reference type) (intercepted)")]
    [ClassData(typeof(TwoParameterReferenceTypeData))]
    public void PropertyIndexedGetterReferenceTypeIntercepted<T>(T? expectedResult, T? expectedValue)
        where T : class
    {
        // Given
        var proxyFactory = Context.ProxyFactory;
        var interceptor = new GetterInterceptor<T>(expectedResult, true);
        var decoratee = new FooReferenceTypeIndexedGetter<T>(default);

        // When
        var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedGetter<T>>(decoratee, interceptor);
        var result = foo[expectedValue];

        // Then
        Assert.NotNull(foo);
        Assert.Equal(0u, decoratee.CallCount);
        Assert.Equal(default, decoratee.Parameter);
        Assert.Equal(expectedResult, result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptPropertyWithName("Item");
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        invocation.ShouldHaveReturnValue(expectedResult);
    }

    #region Interceptor

    private sealed class GetterInterceptor<T> : IInterceptor
    {
        public GetterInterceptor([AllowNull] T result, bool wasIntercepted)
        {
            Result = result;
            WasIntercepted = wasIntercepted;
        }

        private bool WasIntercepted { get; }

        [AllowNull, MaybeNull]
        public T Result { get; }

        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IReturnValue<T>>(out var getterInvocation))
            {
                getterInvocation.ReturnValue = Result;
            }

            return WasIntercepted;
        }
    }

    #endregion
}
