namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
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
    /// Automated tests for the <see cref="DecorateGetterSetterEmitter{T}"/> type.
    /// </summary>
    public sealed class DecorateGetterSetterEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public DecorateGetterSetterEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Getter/Setter (value type)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void PropertyValueType<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(default, false);
            var decoratee = new FooValueTypeProperty<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooValueTypeProperty<T>>(decoratee, interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.GetterCallCount);
            Assert.Equal(1u, decoratee.SetterCallCount);
            Assert.Equal(expectedValue, decoratee.Value);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(default(T));
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Getter/Setter (value type) (intercepted)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void PropertyValueTypeIntercepted<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult, true);
            var decoratee = new FooValueTypeProperty<T>(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooValueTypeProperty<T>>(decoratee, interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.GetterCallCount);
            Assert.Equal(0u, decoratee.SetterCallCount);
            Assert.Equal(default, decoratee.Value);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Indexed Getter/Setter (value type)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void IndexedPropertyValueType<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(default, false);
            var decoratee = new FooValueTypeIndexedProperty<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedProperty<T>>(decoratee, interceptor);
            var result = foo[expectedValue];
            foo[expectedValue] = expectedResult;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.GetterCallCount);
            Assert.Equal(1u, decoratee.SetterCallCount);
            Assert.Equal(expectedResult, decoratee.Value);
            Assert.Equal(expectedValue, decoratee.GetterParameter);
            Assert.Equal(expectedValue, decoratee.SetterParameter);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveReturnValue(default(T));
            Assert.Equal(expectedResult, result);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHavePropertyValue(typeof(T), expectedResult);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Indexed Getter/Setter (value type) (intercepted)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void IndexedPropertyValueTypeIntercepted<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult, true);
            var decoratee = new FooValueTypeIndexedProperty<T>(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooValueTypeIndexedProperty<T>>(decoratee, interceptor);
            var result = foo[expectedValue];
            foo[expectedValue] = expectedResult;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.GetterCallCount);
            Assert.Equal(0u, decoratee.SetterCallCount);
            Assert.Equal(default, decoratee.Value);
            Assert.Equal(default, decoratee.GetterParameter);
            Assert.Equal(default, decoratee.SetterParameter);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHavePropertyValue(typeof(T), expectedResult);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Getter/Setter (reference type)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void PropertyReferenceType<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(default, false);
            var decoratee = new FooReferenceTypeProperty<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooReferenceTypeProperty<T>>(decoratee, interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.GetterCallCount);
            Assert.Equal(1u, decoratee.SetterCallCount);
            Assert.Equal(expectedValue, decoratee.Value);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(default(T));
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Getter/Setter (reference type) (intercepted)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void PropertyReferenceTypeIntercepted<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult, true);
            var decoratee = new FooReferenceTypeProperty<T>(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooReferenceTypeProperty<T>>(decoratee, interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.GetterCallCount);
            Assert.Equal(0u, decoratee.SetterCallCount);
            Assert.Equal(default, decoratee.Value);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Indexed Getter/Setter (reference type)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void IndexedPropertyReferenceType<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(default, false);
            var decoratee = new FooReferenceTypeIndexedProperty<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedProperty<T>>(decoratee, interceptor);
            var result = foo[expectedValue];
            foo[expectedValue] = expectedResult;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.GetterCallCount);
            Assert.Equal(1u, decoratee.SetterCallCount);
            Assert.Equal(expectedResult, decoratee.Value);
            Assert.Equal(expectedValue, decoratee.GetterParameter);
            Assert.Equal(expectedValue, decoratee.SetterParameter);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveReturnValue(default(T));
            Assert.Equal(expectedResult, result);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHavePropertyValue(typeof(T), expectedResult);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        }

        [Theory(DisplayName = "DecorateGetterSetterEmitter: Indexed Getter/Setter (reference type) (intercepted)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void IndexedPropertyReferenceTypeIntercepted<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult, true);
            var decoratee = new FooReferenceTypeIndexedProperty<T>(default);

            // When
            var foo = proxyFactory.CreateDecorator<IFooReferenceTypeIndexedProperty<T>>(decoratee, interceptor);
            var result = foo[expectedValue];
            foo[expectedValue] = expectedResult;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(0u, decoratee.GetterCallCount);
            Assert.Equal(0u, decoratee.SetterCallCount);
            Assert.Equal(default, decoratee.Value);
            Assert.Equal(default, decoratee.GetterParameter);
            Assert.Equal(default, decoratee.SetterParameter);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHavePropertyValue(typeof(T), expectedResult);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
        }

        #region Interceptor

        private sealed class PropertyInterceptor<T> : IInterceptor
        {
            public PropertyInterceptor([AllowNull] T result, bool wasIntercepted)
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
}