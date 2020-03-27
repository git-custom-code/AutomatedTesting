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
    /// Automated tests for the <see cref="DecorateActionEmitter"/> type.
    /// </summary>
    public sealed partial class DecorateActionEmitterTests
    {
        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single input parameter")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterIn<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(false);
            var decoratee = new FooActionValueTypeParameterIn<T>();

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterIn<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single input parameter (intercepted)")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterInIntercepted<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(true);
            var decoratee = new FooActionValueTypeParameterIn<T>();

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterIn<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Empty(decoratee.Parameters);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single ref parameter")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterRef<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(false);
            var decoratee = new FooActionValueTypeParameterRef<T>();
            var actualValue = expectedValue;

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterRef<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(ref actualValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
            Assert.Equal(default, actualValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single ref parameter (intercepted)")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterRefIntercepted<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(true);
            var decoratee = new FooActionValueTypeParameterRef<T>();
            var actualValue = expectedValue;

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterRef<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(ref actualValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Empty(decoratee.Parameters);
            Assert.Equal(expectedValue, actualValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRefCountOf(1);
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterOut();
        }

        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single out parameter")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterOut<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(false);
            var decoratee = new FooActionValueTypeParameterOut<T>(expectedValue);

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterOut<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(out var outValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, outValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterOut<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
        }

        [Theory(DisplayName = "DecorateActionEmitter: Action (value type) with single out parameter (intercepted)")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterOutIntercepted<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor(true);
            var decoratee = new FooActionValueTypeParameterOut<T>(expectedValue);

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterOut<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(out var outValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.CallCount);
            Assert.Equal(default, outValue);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionValueTypeParameterOut<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
            invocation.ShouldHaveParameterOutCountOf(1);
            invocation.ShouldHaveParameterOut("first", typeof(T), default(T));
        }
    }
}