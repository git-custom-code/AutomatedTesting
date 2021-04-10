namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
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
            AssemblyEmitter = assemblyEmitter ?? throw new ArgumentNullException(nameof(assemblyEmitter));
        }

        /// <summary>
        /// Gets an <see cref="IAssemblyEmitter"/> implementation that is used to dynamically create a proxy
        /// at runtime using Reflection.Emit.
        /// </summary>
        private IAssemblyEmitter AssemblyEmitter { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets a thread-safe cache to prevent that the same dynamic partial proxy type is being created multiple times.
        /// </summary>
        private ConcurrentDictionary<Type, Type> PartialProxyTypeCache { get; } = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// Gets a thread-safe cache to prevent that the same dynamic proxy type is being created multiple times.
        /// </summary>
        private ConcurrentDictionary<Type, Type> ProxyTypeCache { get; } = new ConcurrentDictionary<Type, Type>();

        #endregion

        #region Logic

        /// <inheritdoc cref="IDynamicProxyFactory" />
        public T CreateDecorator<T>(T decoratee, IInterceptor interceptor) where T : notnull
        {
            Ensures.NotNull(interceptor, nameof(interceptor));

            var signature = typeof(T);
            var decorator = CreateDecorator(signature, decoratee, interceptor);
            if (decorator is T typedDecorator)
            {
                return typedDecorator;
            }

            throw new Exception($"Unable to create a decorator for interface {signature.Name}");
        }

        /// <inheritdoc cref="IDynamicProxyFactory" />
        public object CreateDecorator(Type signature, object decoratee, IInterceptor interceptor)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(decoratee, nameof(decoratee));
            Ensures.NotNull(interceptor, nameof(interceptor));

            try
            {
                var decoratorType = PartialProxyTypeCache.GetOrAdd(signature, EmitPartialProxyTypeFor);
                var decorator = Activator.CreateInstance(decoratorType, new[] { decoratee, interceptor });
                if (decorator != null)
                {
                    return decorator;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to create a decorator for interface {signature.Name}", e);
            }

            throw new Exception($"Unable to create a decorator for interface {signature.Name}");
        }

        /// <inheritdoc cref="IDynamicProxyFactory" />
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

        /// <inheritdoc cref="IDynamicProxyFactory" />
        public object CreateForInterface(Type signature, IInterceptor interceptor)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(interceptor, nameof(interceptor));

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
        /// Emits a new dynamic partial proxy type for an interface with the the given <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature"> The signature of the interface that should be implemented by the partial proxy. </param>
        /// <returns>
        /// The dynamic partial proxy type that implements an interface with the given <paramref name="signature"/>.
        /// </returns>
        private Type EmitPartialProxyTypeFor(Type signature)
        {
            var proxyName = $"{signature.FullName}PartialMock";
            var dynamicType = AssemblyEmitter.EmitDecoratorType(proxyName);
            dynamicType.ImplementDecorator(signature);
            var proxyType = dynamicType.ToType();
            return proxyType;
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