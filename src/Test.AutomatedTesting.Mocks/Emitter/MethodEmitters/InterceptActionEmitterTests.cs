namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Extensions;
    using Interception;
    using Interception.Parameters;
    using LightInject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="InterceptActionEmitter"/> type.
    /// </summary>
    public sealed partial class InterceptActionEmitterTests
    {
        [Fact(DisplayName = "MethodEmitter: Action without parameters")]
        public void ActionWithoutParameters()
        {
            // Given
            var proxyFactory = CreateFactory();
            var interceptor = new ActionInterceptor();

            // When
            var foo = proxyFactory.CreateForInterface<IFooActionParameterless>(interceptor);
            foo.MethodWithoutParameter();

            // Then
            Assert.NotNull(foo);

            Assert.Single(interceptor.ForwardedInvocations);
            var invocation = interceptor.ForwardedInvocations.Single();
            invocation.ShouldInterceptMethodWithName(nameof(IFooActionParameterless.MethodWithoutParameter));
            invocation.ShouldHaveNoParameterIn();
            invocation.ShouldHaveNoParameterRef();
        }

        #region Logic

        /// <summary>
        /// Creates a new <see cref="IDynamicProxyFactory"/> instance.
        /// </summary>
        /// <returns> The newly created instance. </returns>
        private IDynamicProxyFactory CreateFactory()
        {
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IDynamicProxyFactory).Assembly);
            var proxyFactory = iocContainer.GetInstance<IDynamicProxyFactory>();
            return proxyFactory;
        }

        #endregion

        #region Interceptor

        private sealed class ActionInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
            }
        }

        private sealed class ReplaceRefParameterInterceptor : IInterceptor
        {
            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public void Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IParameterRef>(out var parameterRef))
                {
                    foreach (var parameter in parameterRef.RefParameterCollection)
                    {
                        if (parameter.Type.IsValueType)
                        {
                            parameter.Value = Activator.CreateInstance(parameter.Type);
                        }
                        else
                        {
                            parameter.Value = null;
                        }
                    }
                }
            }
        }

        #endregion
    }
}