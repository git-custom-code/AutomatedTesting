namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Context;
    using Interception;
    using System.Collections.Generic;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="DecorateActionEmitter"/> type.
    /// </summary>
    public sealed partial class DecorateActionEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public DecorateActionEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        [Fact(DisplayName = "MethodDecorateEmitter: Action without parameters")]
        public void ActionWithoutParameters()
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor();
            var decoratee = new FooActionParameterless();

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionParameterless>(decoratee, interceptor);
            foo.MethodWithoutParameter();

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
        }

        #region Interceptor

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