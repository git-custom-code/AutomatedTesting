namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using Interception.Parameters;
    using LightInject;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InterceptActionEmitter"/> type.
    /// </summary>
    public sealed class InterceptActionEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic method implementation for a value type interface action")]
        public void EmitMethodImplementationForValueTypeInterfaceAction()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ActionInterceptor();
            var expectedValueType = 13;

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithValueTypeAction>(interceptor);
            foo.MethodWithOneParameter(expectedValueType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.Equal(nameof(IFooWithValueTypeAction.MethodWithOneParameter), invocation.Signature.Name);
            var inputParameter = invocation.GetFeature<IParameterIn>();
            Assert.Single(inputParameter.InputParameterCollection);
            var parameter = inputParameter.InputParameterCollection.FirstOrDefault();
            Assert.Equal("first", parameter.Name);
            Assert.Equal(typeof(int), parameter.Type);
            Assert.Equal(expectedValueType, parameter.Value);
        }

        [Fact(DisplayName = "Emit a dynamic method implementation for a reference type interface action")]
        public void EmitMethodImplementationForReferenceTypeInterfaceAction()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            var interceptor = new ActionInterceptor();
            var expectedReferencType = new object();

            // When
            var foo = proxyFactory.CreateForInterface<IFooWithReferenceTypeAction>(interceptor);
            foo.MethodWithOneParameter(expectedReferencType);

            // Then
            Assert.NotNull(foo);
            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            Assert.Equal(nameof(IFooWithReferenceTypeAction.MethodWithOneParameter), invocation.Signature.Name);
            var inputParameter = invocation.GetFeature<IParameterIn>();
            Assert.Single(inputParameter.InputParameterCollection);
            var parameter = inputParameter.InputParameterCollection.FirstOrDefault();
            Assert.Equal("first", parameter.Name);
            Assert.Equal(typeof(object), parameter.Type);
            Assert.Equal(expectedReferencType, parameter.Value);
        }

        #region Mocks

        private sealed class ActionInterceptor : IInterceptor
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