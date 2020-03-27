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
    /// Automated tests for the <see cref="DecorateFuncEmitter{T}"/> type.
    /// </summary>
    public sealed partial class DecorateFuncEmitterTests
    {
        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) without parameters")]
        [ClassData(typeof(ReferenceTypeData))]
        public void FuncReferenceTypeWithoutParameters<T>(T? expectedResult)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(default, false);
            var decoratee = new FooFuncReferenceTypeParameterless<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterless<T>>(decoratee, interceptor);
            var result = foo.MethodWithoutParameter();

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterless<T>.MethodWithoutParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) without parameters (intercepted)")]
        [ClassData(typeof(ReferenceTypeData))]
        public void FuncReferenceTypeWithoutParametersIntercepted<T>(T? expectedResult)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(default, true);
            var decoratee = new FooFuncReferenceTypeParameterless<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterless<T>>(decoratee, interceptor);
            var result = foo.MethodWithoutParameter();

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(default, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterless<T>.MethodWithoutParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single input parameter")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterIn<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(default, false);
            var decoratee = new FooFuncReferenceTypeParameterIn<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterIn<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single input parameter (intercepted)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterInIntercepted<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(default, true);
            var decoratee = new FooFuncReferenceTypeParameterIn<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterIn<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Empty(decoratee.Parameters);
            Assert.Equal(default, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single ref parameter")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterRef<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult, false);
            var decoratee = new FooFuncReferenceTypeParameterRef<T>(expectedResult);
            var actualValue = expectedValue;

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterRef<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(ref actualValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
            Assert.Equal(expectedResult, result);
            Assert.Equal(default, actualValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single ref parameter (intercepted)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterRefIntercepted<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult, true);
            var decoratee = new FooFuncReferenceTypeParameterRef<T>(expectedValue);
            var actualValue = expectedValue;

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterRef<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(ref actualValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Empty(decoratee.Parameters);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedValue, actualValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single out parameter")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterOut<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(default, false);
            var decoratee = new FooFuncReferenceTypeParameterOut<T>(expectedResult, expectedValue);
            
            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterOut<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(out var outValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, outValue);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterOut<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
        }

        [Theory(DisplayName = "DecorateFuncEmitter: Func (reference type) with single out parameter (intercepted)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void FuncReferenceTypeWithSingleParameterOutIntercepted<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new FuncInterceptor<T>(expectedResult, true);
            var decoratee = new FooFuncReferenceTypeParameterOut<T>(default, expectedValue);

            // When
            var foo = proxyFactory.CreateDecorator<IFooFuncReferenceTypeParameterOut<T>>(decoratee, interceptor);
            var result = foo.MethodWithOneParameter(out var outValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(default, outValue);
            Assert.Equal(expectedResult, result);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooFuncReferenceTypeParameterOut<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
        }
    }
}