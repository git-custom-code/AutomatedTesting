namespace CustomCode.AutomatedTesting.Mocks.Composition
{
    using ExceptionHandling;
    using Dependencies;
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

        /// <inheritdoc cref="ICompositionRoot" />
        public void Compose(IServiceRegistry serviceRegistry)
        {
            Ensures.NotNull(serviceRegistry, nameof(serviceRegistry));

            serviceRegistry.Register<IDynamicProxyFactory, DynamicProxyFactory>();
            serviceRegistry.Register<IAssemblyEmitter, AssemblyEmitter>();
            serviceRegistry.Register<IDependencyEmitter, DependencyEmitter>();
            serviceRegistry.Register<IMethodEmitterFactory, MethodEmitterFactory>();
            serviceRegistry.Register<IMethodDecoratorEmitterFactory, MethodDecoratorEmitterFactory>();
            serviceRegistry.Register<IPropertyEmitterFactory, PropertyEmitterFactory>();
            serviceRegistry.Register<IPropertyDecoratorEmitterFactory, PropertyDecoratorEmitterFactory>();
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
            serviceRegistry.Register<TypeBuilder, ITypeDecoratorEmitter>((factory, typeBuilder) =>
                {
                    var dependencyEmitter = factory.GetInstance<IDependencyEmitter>();
                    var methodEmitterFactory = factory.GetInstance<IMethodDecoratorEmitterFactory>();
                    var propertyEmitterFactory = factory.GetInstance<IPropertyDecoratorEmitterFactory>();
                    return new TypeDecoratorEmitter(
                        typeBuilder,
                        dependencyEmitter,
                        methodEmitterFactory,
                        propertyEmitterFactory);
                });
            serviceRegistry.Register<IInterceptorFactory, InterceptorFactory>();
            serviceRegistry.Register<IMockedDependencyFactory, MockedDependencyFactory>();
        }

        #endregion
    }
}