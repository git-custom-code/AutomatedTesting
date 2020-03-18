namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Properties;
    using Interception.ReturnValue;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptGetterSetterEmitter"/> type.
    /// </summary>
    public sealed class InterceptGetterSetterEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic value type property implementation for an interface")]
        public void EmitImplementationForValueTypeProperty()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ValueTypeInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeProperties>(interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = 13;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var getterInvocation = interceptor.ForwardedInvocations.First();
            Assert.True(getterInvocation.HasFeature<IPropertyInvocation>());
            var getterFeature = getterInvocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), getterFeature.Signature.Name);
            Assert.Equal(42, result);

            var setterInvocation = interceptor.ForwardedInvocations.Last();
            Assert.True(setterInvocation.HasFeature<IPropertySetterValue>());
            var setterFeature = setterInvocation.GetFeature<IPropertySetterValue>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), setterFeature.Signature.Name);
            Assert.Equal(13, setterFeature.Value);
        }

        [Fact(DisplayName = "Emit a dynamic reference type property implementation for an interface")]
        public void EmitImplementationForReferenceTypeProperty()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ReferenceTypeInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeProperties>(interceptor);
            var result = foo.GetterSetter;
            foo.GetterSetter = typeof(string);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(2, interceptor.ForwardedInvocations.Count);

            var getterInvocation = interceptor.ForwardedInvocations.First();
            Assert.True(getterInvocation.HasFeature<IPropertyInvocation>());
            var getterFeature = getterInvocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), getterFeature.Signature.Name);
            Assert.Equal(typeof(double), result);

            var setterInvocation = interceptor.ForwardedInvocations.Last();
            Assert.True(setterInvocation.HasFeature<IPropertySetterValue>());
            var setterFeature = setterInvocation.GetFeature<IPropertySetterValue>();
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), setterFeature.Signature.Name);
            Assert.Equal(typeof(string), setterFeature.Value);
        }

        #region Mocks

        private sealed class ValueTypeInterceptor : IInterceptor
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

        private sealed class ReferenceTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<object?>>(out var getterInvocation))
                {
                    getterInvocation.ReturnValue = typeof(double);
                }
            }
        }

        #endregion
    }
}