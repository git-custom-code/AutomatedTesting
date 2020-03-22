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

        #region Interceptor

        private sealed class SetterInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
            }
        }

        #endregion
    }
}