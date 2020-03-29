namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using TestDomain;
    using LightInject;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="TypeDecoratorEmitter"/> type.
    /// </summary>
    public sealed class TypeDecoratorEmitterTests
    {
        [Fact(DisplayName = "TypeDecoratorEmitter: Emit decorator implementation for dynamic proxy")]
        public void EmitDynamicTypeAsDecoratorProxy()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(ITypeEmitter).Assembly);
            var asmEmitter = iocContainer.GetInstance<IAssemblyEmitter>();

            // When
            var emitter = asmEmitter.EmitDecoratorType("My.Namespace.MyType");
            emitter.ImplementDecorator<IFoo>();
            var type = emitter.ToType();

            // Then
            Assert.NotNull(emitter);
            Assert.NotNull(type);
            Assert.Equal("My.Namespace", type.Namespace);
            Assert.Equal("MyType", type.Name);
            Assert.Equal(typeof(IFoo), type.GetInterface(nameof(IFoo)));
        }

        [Fact(DisplayName = "TypeDecoratorEmitter: Emit a dynamic proxy")]
        public void EmitDynamicTypeAtRuntime()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(ITypeEmitter).Assembly);
            var asmEmitter = iocContainer.GetInstance<IAssemblyEmitter>();

            // When
            var emitter = asmEmitter.EmitDecoratorType("My.Namespace.MyType");
            var type = emitter.ToType();

            // Then
            Assert.NotNull(emitter);
            Assert.NotNull(type);
            Assert.Equal("My.Namespace", type.Namespace);
            Assert.Equal("MyType", type.Name);
        }
    }
}