namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
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
            var getterInvocation = interceptor.ForwardedInvocations.First() as GetterInvocation;
            Assert.NotNull(getterInvocation);
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), getterInvocation?.PropertySignature.Name);
            Assert.Equal(42, result);
            var setterInvocation = interceptor.ForwardedInvocations.Last() as SetterInvocation;
            Assert.NotNull(setterInvocation);
            Assert.Equal(nameof(IFooWithValueTypeProperties.GetterSetter), setterInvocation?.PropertySignature.Name);
            Assert.Equal(13, setterInvocation?.Value);
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
            var getterInvocation = interceptor.ForwardedInvocations.First() as GetterInvocation;
            Assert.NotNull(getterInvocation);
            Assert.Equal(nameof(IFooWithReferenceTypeProperties.GetterSetter), getterInvocation?.PropertySignature.Name);
            Assert.NotNull(result);
            Assert.Equal(typeof(double), result);
            var setterInvocation = interceptor.ForwardedInvocations.Last() as SetterInvocation;
            Assert.NotNull(setterInvocation);
            Assert.Equal(nameof(IFooWithReferenceTypeProperties.GetterSetter), setterInvocation?.PropertySignature.Name);
            Assert.Equal(typeof(string), setterInvocation?.Value);
        }

        #region Mocks

        private sealed class ValueTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation is GetterInvocation getterInvocation)
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
                if (invocation is GetterInvocation getterInvocation)
                {
                    getterInvocation.ReturnValue = typeof(double);
                }
            }
        }

        #endregion
    }
}