namespace CustomCode.AutomatedTesting.Mocks.Composition
{
    using Emitter;
    using Interception;
    using LightInject;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="ICompositionRoot"/> interface that registers the needed service for
    /// lightinject's inversion of control container.
    /// </summary>
    public sealed class CompositionRoot : ICompositionRoot
    {
        #region Logic

        /// <inheritdoc />
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDynamicProxyFactory, DynamicProxyFactory>();
            serviceRegistry.Register<IAssemblyEmitter, AssemblyEmitter>();
            serviceRegistry.Register<IDependencyEmitter, DependencyEmitter>();
            serviceRegistry.Register<IMethodEmitterFactory, MethodEmitterFactory>();
            serviceRegistry.Register<IPropertyEmitterFactory, PropertyEmitterFactory>();
            serviceRegistry.Register<TypeBuilder, ITypeEmitter>((factory, typeBuilder) =>
                {
                    var dependencyEmitter = factory.GetInstance<IDependencyEmitter>();
                    var methodEmitterFactory = factory.GetInstance<IMethodEmitterFactory>();
                    var propertyEmitterFactory = factory.GetInstance<IPropertyEmitterFactory>();
                    return new TypeEmitter(
                        typeBuilder,
                        dependencyEmitter,
                        methodEmitterFactory,
                        propertyEmitterFactory);
                });
            serviceRegistry.Register<IInterceptorFactory, InterceptorFactory>();
        }

        #endregion
    }
}