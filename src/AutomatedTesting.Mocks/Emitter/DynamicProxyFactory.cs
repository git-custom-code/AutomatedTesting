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
            var signature = typeof(T);
            var proxy = CreateForInterface(signature, interceptor);
            if (proxy is T typedProxy)
            {
                return typedProxy;
            }

            throw new Exception($"Unable to create a proxy for interface {signature.Name}");
        }

        /// <inheritdoc />
        public object CreateForInterface(Type signature, IInterceptor interceptor)
        {
            try
            {
                var proxyType = ProxyTypeCache.GetOrAdd(signature, EmitProxyTypeFor);
                var proxy = Activator.CreateInstance(proxyType, new[] { interceptor });
                if (proxy != null)
                {
                    return proxy;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to create a proxy for interface {signature.Name}", e);
            }

            throw new Exception($"Unable to create a proxy for interface {signature.Name}");
        }

        /// <summary>
        /// Emits a new dynamic proxy type for an interface with the the given <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"> The signature of the interface that should be implemented by the proxy. </param>
        /// <returns>
        /// The dynamic proxy type that implements an interface with the given <paramref name="signature"/>.
        /// </returns>
        private Type EmitProxyTypeFor(Type signature)
        {
            var proxyName = $"{signature.FullName}Mock";
            var dynamicType = AssemblyEmitter.EmitType(proxyName);
            dynamicType.ImplementInterface(signature);
            var proxyType = dynamicType.ToType();
            return proxyType;
        }

        #endregion
    }
}