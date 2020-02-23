namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var foo = proxyFactory.CreateForInterface<IFooWithValueType>(interceptor);
            var result = foo.Bar;
            foo.Bar = 13.0;

            // Then
            Assert.NotNull(foo);
            Assert.Equal(2, interceptor.ForwardedInvocations.Count);
            var getterInvocation = interceptor.ForwardedInvocations.First() as GetterInvocation;
            Assert.NotNull(getterInvocation);
            Assert.Equal(nameof(IFooWithValueType.Bar), getterInvocation?.PropertySignature.Name);
            Assert.Equal(42, result);
            var setterInvocation = interceptor.ForwardedInvocations.Last() as SetterInvocation;
            Assert.NotNull(setterInvocation);
            Assert.Equal(nameof(IFooWithValueType.Bar), setterInvocation?.PropertySignature.Name);
            Assert.Equal(13.0, setterInvocation?.Value);
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
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceType>(interceptor);
            var result = foo.Bar;
            foo.Bar = typeof(string);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(2, interceptor.ForwardedInvocations.Count);
            var getterInvocation = interceptor.ForwardedInvocations.First() as GetterInvocation;
            Assert.NotNull(getterInvocation);
            Assert.Equal(nameof(IFooWithReferenceType.Bar), getterInvocation?.PropertySignature.Name);
            Assert.NotNull(result);
            Assert.Equal(typeof(double), result);
            var setterInvocation = interceptor.ForwardedInvocations.Last() as SetterInvocation;
            Assert.NotNull(setterInvocation);
            Assert.Equal(nameof(IFooWithReferenceType.Bar), setterInvocation?.PropertySignature.Name);
            Assert.Equal(typeof(string), setterInvocation?.Value);
        }

        #region Domain

        public interface IFooWithValueType
        {
            double Bar { get; set; }
        }

        public interface IFooWithReferenceType
        {
            Type Bar { get; set; }
        }

        #endregion

        #region Mocks

        public sealed class ValueTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation is GetterInvocation getterInvocation)
                {
                    getterInvocation.ReturnValue = 42.0;
                }
            }
        }

        public sealed class ReferenceTypeInterceptor : IInterceptor
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