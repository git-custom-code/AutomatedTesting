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
    /// Automated tests for the <see cref="InterceptActionEmitter"/> type.
    /// </summary>
    public sealed partial class InterceptActionEmitterTests
    {
        [Theory(DisplayName = "MethodEmitter: Action (reference type) with single input parameter")]
        [ClassData(typeof(ReferenceTypeData))]
        public void ActionReferenceTypeWithSingleParameterIn<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ActionInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeParameterIn<T>>(interceptor);
            foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with single ref parameter")]
        [ClassData(typeof(ReferenceTypeData))]
        public void ActionReferenceTypeWithSingleParameterRef<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ActionInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeParameterRef<T>>(interceptor);
            foo.MethodWithOneParameter(ref expectedValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeParameterIn<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRef("first", typeof(T), expectedValue);
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with replaced single ref parameter")]
        [ClassData(typeof(ReferenceTypeData))]
        public void ActionReferenceTypeWithReplacedSingleParameterRef<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ReplaceRefParameterInterceptor();
            var replacedRefValue = expectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeParameterRef<T>>(interceptor);
            foo.MethodWithOneParameter(ref replacedRefValue);

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeParameterRef<T>.MethodWithOneParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            Assert.Equal(default, replacedRefValue);
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with overloaded method (first overload)")]
        [ClassData(typeof(ReferenceTypeData))]
        public void ActionReferenceTypeWithFirstOverloadedMethod<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ActionInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeOverloadsIn<T>>(interceptor);
            foo.MethodWithOverload(expectedValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveNoParameterRef();
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with overloaded method (second overload)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void ActionReferenceTypeWithSecondOverloadedMethod<T>(T? firstExpectedValue, T? secondExpectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ActionInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeOverloadsIn<T>>(interceptor);
            foo.MethodWithOverload(firstExpectedValue, secondExpectedValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveParameterIn("first", typeof(T), firstExpectedValue);
            invocation.ShouldHaveParameterIn("second", typeof(T), secondExpectedValue);
            invocation.ShouldHaveNoParameterRef();
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with overloaded method (first overload)")]
        [ClassData(typeof(ReferenceTypeData))]
        public void ActionReferenceTypeWithFirstOverloadedMethodWithReplacedRef<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ReplaceRefParameterInterceptor();
            var replacedRefValue = expectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeOverloadsRef<T>>(interceptor);
            foo.MethodWithOverload(ref replacedRefValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            Assert.Equal(default, replacedRefValue);
        }

        [Theory(DisplayName = "MethodEmitter: Action (reference type) with overloaded method (second overload)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void ActionReferenceTypeWithSecondOverloadedMethodWithReplacedRef<T>(T? firstExpectedValue, T? secondExpectedValue)
            where T : class
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ReplaceRefParameterInterceptor();
            var replacedFirstRefValue = firstExpectedValue;
            var replacedSecondRefValue = secondExpectedValue;

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionReferenceTypeOverloadsRef<T>>(interceptor);
            foo.MethodWithOverload(ref replacedFirstRefValue, ref replacedSecondRefValue);

            // Then
            Assert.NotNull(foo);

            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionReferenceTypeOverloadsIn<T>.MethodWithOverload));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveParameterRef("first", typeof(T), default(T));
            invocation.ShouldHaveParameterRef("second", typeof(T), default(T));
            Assert.Equal(default, replacedFirstRefValue);
            Assert.Equal(default, replacedSecondRefValue);
        }
    }
}