namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception;
    using LightInject;
    using System.Collections.Generic;

    /// <summary>
    /// Automated tests for the <see cref="InterceptAsyncMethodEmitter{T}"/> type.
    /// </summary>
    public sealed partial class InterceptAsyncMethodEmitterTests
    {
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

        private sealed class AsyncInterceptor : IInterceptor
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