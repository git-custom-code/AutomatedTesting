namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptFuncEmitter"/> type.
    /// </summary>
    public sealed class InterceptFuncEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for an interface method that returns a value type")]
        public void EmitImplementationForMethodThatReturnsValueType()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new FuncWithValueTypeInterceptor();
            var expectedValueType = 0;
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeFunc>(interceptor);
            var result = foo.Bar(expectedValueType, expectedReferenceType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as FuncInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithValueTypeFunc.Bar), invocation?.Signature.Name);
            Assert.Equal(42, result);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for an interface method that returns a reference type")]
        public void EmitImplementationForMethodThatReturnsReferenceType()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new FuncWithReferenceTypeInterceptor();
            var expectedValueType = 0;
            var expectedReferenceType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeFunc>(interceptor);
            var result = foo.Bar(expectedValueType, expectedReferenceType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single() as FuncInvocation;
            Assert.NotNull(invocation);
            Assert.Equal(nameof(IFooWithReferenceTypeFunc.Bar), invocation?.Signature.Name);
            Assert.NotNull(result);
            Assert.Equal("Foo", result.ToString());
        }

        #region Domain

        public interface IFooWithValueTypeFunc
        {
            double Bar(int @valueType, object @referenceType);
        }

        public interface IFooWithReferenceTypeFunc
        {
            StringBuilder Bar(int @valueType, object @referenceType);
        }

        #endregion

        #region Mocks

        public sealed class FuncWithValueTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation is FuncInvocation funcInvocation)
                {
                    funcInvocation.ReturnValue = 42.0;
                }
            }
        }

        public sealed class FuncWithReferenceTypeInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation is FuncInvocation funcInvocation)
                {
                    funcInvocation.ReturnValue = new StringBuilder("Foo");
                }
            }
        }

        #endregion
    }
}