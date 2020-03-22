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
    /// Automated tests for the <see cref="InterceptGetterSetterEmitter{T}"/> type.
    /// </summary>
    public sealed class InterceptGetterSetterEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public InterceptGetterSetterEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        [Theory(DisplayName = "PropertyEmitter: Getter/Setter (value type)")]
        [ClassData(typeof(TwoParameterValueTypeData))]
        public void PropertyGetterValueType<T>(T expectedResult, T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooValueTypeProperty<T>>(interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooValueTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        [Theory(DisplayName = "PropertyEmitter: Getter/Setter (reference type)")]
        [ClassData(typeof(TwoParameterReferenceTypeData))]
        public void PropertyGetterReferenceType<T>(T? expectedResult, T? expectedValue)
            where T : class
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new PropertyInterceptor<T>(expectedResult);

            // When
            var foo = proxyFactory.CreateForInterface<IFooReferenceTypeProperty<T>>(interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = expectedValue;

            // Then
            Assert.NotNull(foo);

            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var invocation = interceptor.ForwardedInvocations.First();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHaveReturnValue(expectedResult);
            Assert.Equal(expectedResult, result);

            invocation = interceptor.ForwardedInvocations.Last();
            invocation.ShouldInterceptPropertyWithName(nameof(IFooReferenceTypeProperty<T>.GetterSetter));
            invocation.ShouldHavePropertyValue(typeof(T), expectedValue);
        }

        #region Interceptor

        private sealed class PropertyInterceptor<T> : IInterceptor
        {
            public PropertyInterceptor([AllowNull] T result)
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