namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Properties;
    using Interception.ReturnValue;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptGetterEmitter{T}"/> type.
    /// </summary>
    public sealed class InterceptGetterEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic property implementation for an interface property getter that returns a value type")]
        public void EmitImplementationForPropertyGetterThatReturnsValueType()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new GetterWithValueTypeInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeProperties>(interceptor);
            var result = foo.Getter;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.True(invocation.HasFeature<IPropertyInvocation>());
            var feature = invocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.Getter), feature.Signature.Name);
            Assert.Equal(42, result);
        }

        [Fact(DisplayName = "Emit a dynamic property implementation for an interface property getter that returns a reference type")]
        public void EmitImplementationForProperytGetterThatReturnsReferenceType()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new GetterWithReferenceTypeInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeProperties>(interceptor);
            var result = foo.Getter;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.True(invocation.HasFeature<IPropertyInvocation>());
            var feature = invocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithReferenceTypeProperties.Getter), feature.Signature.Name);
            Assert.NotNull(result);
            Assert.Equal("Foo", result?.ToString());
        }

        #region Mocks

        private sealed class GetterWithValueTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<int>>(out var getterInvocation))
                {
                    getterInvocation.ReturnValue = 42;
                }
            }
        }

        private sealed class GetterWithReferenceTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<object?>>(out var getterInvocation))
                {
                    getterInvocation.ReturnValue = new StringBuilder("Foo");
                }
            }
        }

        #endregion
    }
}