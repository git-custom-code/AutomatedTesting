namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Default implementation of the <see cref="IDynamicProxyFactory"/> interface.
    /// </summary>
    public sealed class DynamicProxyFactory : IDynamicProxyFactory
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="DynamicProxyFactory"/> type.
        /// </summary>
        /// <param name="assemblyEmitter">
        /// An <see cref="IAssemblyEmitter"/> implementation that is used to dynamically create a proxy at
        /// runtime using Reflection.Emit.
        /// </param>
        public DynamicProxyFactory(IAssemblyEmitter assemblyEmitter)
        {
            AssemblyEmitter = assemblyEmitter;
        }

        /// <summary>
        /// Gets an <see cref="IAssemblyEmitter"/> implementation that is used to dynamically create a proxy
        /// at runtime using Reflection.Emit.
        /// </summary>
        private IAssemblyEmitter AssemblyEmitter { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets a thread-safe cache to prevent that the same dynamic proxy type is being created multiple times.
        /// </summary>
        private ConcurrentDictionary<Type, Type> ProxyTypeCache { get; } = new ConcurrentDictionary<Type, Type>();

        #endregion

        #region Logic

        /// <inheritdoc />
        public T CreateForInterface<T>(IInterceptor interceptor) where T : notnull
        {
            var @interface = typeof(T);
            var proxy = CreateForInterface(@interface, interceptor);
            if (proxy is T typedProxy)
            {
                return typedProxy;
            }

            throw new Exception($"Unable to create a proxy for interface {typeof(T).Name}");
        }

        /// <inheritdoc />
        public object CreateForInterface(Type @interface, IInterceptor interceptor)
        {
            try
            {
                var proxyType = ProxyTypeCache.GetOrAdd(@interface, EmitProxyTypeFor);
                var proxy = Activator.CreateInstance(proxyType, new[] { interceptor });
                if (proxy != null)
                {
                    return proxy;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to create a proxy for interface {@interface.Name}", e);
            }

            throw new Exception($"Unable to create a proxy for interface {@interface.Name}");
        }

        /// <summary>
        /// Emits a new dynamic proxy type for the given<paramref name="interface"/>.
        /// </summary>
        /// <param name="interface"> The interface that should be implemented by the proxy. </param>
        /// <returns> The dynamic proxy type that implements the given <paramref name="interface"/>. </returns>
        private Type EmitProxyTypeFor(Type @interface)
        {
            var proxyName = $"{@interface.FullName}Mock";
            var dynamicType = AssemblyEmitter.EmitType(proxyName);
            dynamicType.ImplementInterface(@interface);
            var proxyType = dynamicType.ToType();
            return proxyType;
        }

        #endregion
    }
}