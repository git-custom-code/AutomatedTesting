namespace CustomCode.AutomatedTesting.Mocks.Core.Context
{
    using Emitter;
    using LightInject;
    using System;
    using Xunit;

    /// <summary>
    /// Allows the usage of an <see cref="IDynamicProxyFactory"/> instance as
    /// <see cref="IClassFixture{TFixture}"/> (for test performance).
    /// </summary>
    public sealed class ProxyFactoryContext : IDisposable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ProxyFactoryContext"/> type.
        /// </summary>
        public ProxyFactoryContext()
        {
            ProxyFactory = CreateFactory();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a shared factory for creating dynamic proxy instances.
        /// </summary>
        public IDynamicProxyFactory ProxyFactory { get; }

        #endregion

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

        /// <inheritdoc />
        public void Dispose()
        { }

        #endregion
    }
}