namespace CustomCode.AutomatedTesting.Mocks.Composition
{
    using Emitter;
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
            serviceRegistry.Register<IAssemblyEmitter, AssemblyEmitter>();
            serviceRegistry.Register<TypeBuilder, ITypeEmitter>((factory, typeBuilder) =>
                {
                    var dependencyEmitter = factory.GetInstance<IDependencyEmitter>();
                    return new TypeEmitter(typeBuilder, dependencyEmitter);
                });
            serviceRegistry.Register<IDependencyEmitter, DependencyEmitter>();
        }

        #endregion
    }
}