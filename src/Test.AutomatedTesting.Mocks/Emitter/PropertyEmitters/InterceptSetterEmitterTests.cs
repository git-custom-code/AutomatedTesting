namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
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
    /// Automated tests for the <see cref="InterceptSetterEmitter"/> type.
    /// </summary>
    public sealed class InterceptSetterEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public InterceptSetterEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        [Theory(DisplayName = "PropertyEmitter: Setter (value type)")]
        [ClassData(typeof(ValueTypeData))]
        public void PropertySetterValueType<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTypeSetter<T>>(interceptor);
            foo.Setter = expectedValue;

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeSetter<T>.Setter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "PropertyEmitter: Indexed Setter (value type)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void PropertyIndexedSetterValueType<T>(T expectedValue, T expectedIndex)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTypeIndexedSetter<T>>(interceptor);
            foo[expectedIndex] = expectedValue;

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedIndex);
        }

        [Theory(DisplayName = "PropertyEmitter: Setter (reference type)")]
        [ClassData(typeof(ReferenceTypeData))]
        public void PropertySetterReferenceType<T>(T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooReferenceTypeSetter<T>>(interceptor);
            foo.Setter = expectedValue;

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeSetter<T>.Setter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "PropertyEmitter: Indexed Setter (reference type)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void PropertyIndexedSetterReferenceType<T>(T? expectedValue, T? expectedIndex)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooReferenceTypeIndexedSetter<T>>(interceptor);
            foo[expectedIndex] = expectedValue;

            // Then
            Assert.NotNull(foo);

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
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                return true;
            }
        }

        #endregion
    }
}