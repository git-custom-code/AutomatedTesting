namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
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
        [Theory(DisplayName = "MethodEmitter: Func (value type) without parameters")]
        [ClassData(typeof(ValueTypeData))]
        public void FuncValueTypeWithoutParameters<T>(T expectedResult)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeParameterless<T>>(interceptor);
            var result = foo.MethodWithoutParameter();

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterless<T>.MethodWithoutParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with single input parameter")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithSingleParameterIn<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeParameterIn<T>>(interceptor);
            var result = foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with single ref parameter")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FunctValueTypeWithSingleParameterRef<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeParameterRef<T>>(interceptor);
            var result = foo.MethodWithOneParameter(ref expectedValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with replaced single ref parameter")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithReplacedSingleParameterRef<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
            var replacedRefValue = expectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeParameterRef<T>>(interceptor);
            var result = foo.MethodWithOneParameter(ref replacedRefValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            Assert.Equal(default, replacedRefValue);
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with single out parameter")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithSingleParameterOut<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeParameterOut<T>>(interceptor);
            var result = foo.MethodWithOneParameter(out var outValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeParameterOut<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
            Assert.Equal(expectedValue, outValue);
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded method (first overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithFirstOverloadedMethod<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsIn<T>>(interceptor);
            var result = foo.MethodWithOverload(expectedValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded method (second overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithSecondOverloadedMethod<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsIn<T>>(interceptor);
            var result = foo.MethodWithOverload(expectedValue, expectedValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveParameterInCountOf(2);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveParameterIn("second", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded method (first overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithFirstOverloadedMethodWithReplacedRef<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
            var replacedRefValue = expectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsRef<T>>(interceptor);
            var result = foo.MethodWithOverload(ref replacedRefValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsRef<T>.MethodWithOverload));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            Assert.Equal(default, replacedRefValue);
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded method (second overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithSecondOverloadedMethodWithReplacedRef<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ReplaceRefParameterInterceptor<T>(expectedResult);
            var replacedFirstRefValue = expectedValue;
            var replacedSecondRefValue = expectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsRef<T>>(interceptor);
            var result = foo.MethodWithOverload(ref replacedFirstRefValue, ref replacedSecondRefValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsRef<T>.MethodWithOverload));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(2);
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            invocation.ShouldHaveParameterRef("second", typeof(T), default(T));
            Assert.Equal(default, replacedFirstRefValue);
            Assert.Equal(default, replacedSecondRefValue);
            invocation.ShouldHaveNoParameterOut();
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded out parameter method (first overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithFirstOverloadedMethodOut<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsOut<T>>(interceptor);
            var result = foo.MethodWithOverload(out var outValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsOut<T>.MethodWithOverload));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), expectedValue);
            Assert.Equal(expectedValue, outValue);
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "MethodEmitter: Func (value type) with overloaded out parameter method (second overload)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void FuncValueTypeWithSecondOverloadedMethodOut<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new OutParameterInterceptor<T>(expectedResult, expectedValue);

            // When
            var foo = proxyFactory.CreateForInterface<IFooFuncValueTypeOverloadsOut<T>>(interceptor);
            var result = foo.MethodWithOverload(out var firstOutValue, out var secondOutValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncValueTypeOverloadsIn<T>.MethodWithOverload));
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
}