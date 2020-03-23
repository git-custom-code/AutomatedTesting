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
    /// Automated tests for the <see cref="InterceptGetterEmitter{T}"/> type.
    /// </summary>
    public sealed class InterceptGetterEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public InterceptGetterEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        [Theory(DisplayName = "PropertyEmitter: Getter (value type)")]
        [ClassData(typeof(ValueTypeData))]
        public void PropertyGetterValueType<T>(T expectedResult)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new GetterInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTypeGetter<T>>(interceptor);
            var result = foo.Getter;

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeGetter<T>.Getter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "PropertyEmitter: Indexed Getter (value type)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void PropertyIndexedGetterValueType<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new GetterInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTypeIndexedGetter<T>>(interceptor);
            var result = foo[expectedValue];

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "PropertyEmitter: Getter (reference type)")]
        [ClassData(typeof(ReferenceTypeData))]
        public void PropertyGetterReferenceType<T>(T? expectedResult)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new GetterInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooReferenceTypeGetter<T>>(interceptor);
            var result = foo.Getter;

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeGetter<T>.Getter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
        }

        [Theory(DisplayName = "PropertyEmitter: Indexed Getter (reference type)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void PropertyIndexedGetterReferenceType<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new GetterInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooReferenceTypeIndexedGetter<T>>(interceptor);
            var result = foo[expectedValue];

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptPropertyWithName("Item");
            invocation.ShouldHaveParameterInCountOf(1);
            invocation.ShouldHaveParameterIn("first", typeof(T), expectedValue);
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);
        }

        #region Interceptor

        private sealed class GetterInterceptor<T> : IInterceptor
        {
            public GetterInterceptor([AllowNull] T result)
            {
                Result = result;
            }

            [AllowNull, MaybeNull]
            public T Result { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<T>>(out var getterInvocation))
                {
                    getterInvocation.ReturnValue = Result;
                }
            }
        }

        #endregion
    }
}