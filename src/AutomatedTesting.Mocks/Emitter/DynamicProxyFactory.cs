namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;

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

        #region Logic

        /// <inheritdoc />
        public T CreateForInterface<T>(IInterceptor interceptor) where T : class
        {
            try
            {
                var proxyName = $"{typeof(T).FullName}Mock";
                var dynamicType = AssemblyEmitter.EmitType(proxyName);
                dynamicType.ImplementInterface<T>();
                var proxyType = dynamicType.ToType();
                if (Activator.CreateInstance(proxyType, new[] { interceptor }) is T proxy)
                {
                    return proxy;
                }
            }
            catch(Exception e)
            {
                throw new Exception($"Unable to create a proxy for interface {typeof(T).Name}", e);
            }

            throw new Exception($"Unable to create a proxy for interface {typeof(T).Name}");
        }
 
        #endregion
    }
}