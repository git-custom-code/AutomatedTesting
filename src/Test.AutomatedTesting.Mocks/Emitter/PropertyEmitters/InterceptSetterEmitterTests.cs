namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Parameters;
    using Interception.Properties;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptSetterEmitter"/> type.
    /// </summary>
    public sealed class InterceptSetterEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic property implementation for an interface property setter (value type)")]
        public void EmitImplementationForInterfacePropertySetterValueType()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new SetterInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeProperties>(interceptor);
            foo.Setter = 42;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.True(invocation.HasFeature<IPropertyInvocation>());
            var feature = invocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.Setter), feature.Signature.Name);
            Assert.True(invocation.HasFeature<IPropertySetterValue>());
            var valueFeature = invocation.GetFeature<IPropertySetterValue>();
            Assert.Equal(42, valueFeature.Value);
        }

        [Fact(DisplayName = "Emit a dynamic property implementation for an interface property setter (reference type)")]
        public void EmitImplementationForInterfacePropertySetterReferenceType()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new SetterInterceptor();
            var expectedValue = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeProperties>(interceptor);
            foo.Setter = expectedValue;

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.True(invocation.HasFeature<IPropertyInvocation>());
            var feature = invocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.Setter), feature.Signature.Name);
            Assert.True(invocation.HasFeature<IPropertySetterValue>());
            var valueFeature = invocation.GetFeature<IPropertySetterValue>();
            Assert.Equal(expectedValue, valueFeature.Value);
        }

        #region Mocks

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