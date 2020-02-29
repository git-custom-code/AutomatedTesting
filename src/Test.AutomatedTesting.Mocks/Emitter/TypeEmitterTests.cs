namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using TestDomain;
    using LightInject;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="TypeEmitter"/> type.
    /// </summary>
    public sealed class TypeEmitterTests
    {
        [Fact(DisplayName = "Emit a dynamic type as interface proxy")]
        public void EmitDynamicTypeAsInterfaceProxy()
        {
            // Given
            var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(ITypeEmitter).Assembly);
            var asmEmitter = iocContainer.GetInstance<IAssemblyEmitter>();

            // When
            var emitter = asmEmitter.EmitType("My.Namespace.MyType");
            emitter.ImplementInterface<IFoo>();
            var type = emitter.ToType();

            // Then
            Assert.NotNull(emitter);
            Assert.NotNull(type);
            Assert.Equal("My.Namespace", type.Namespace);
            Assert.Equal("MyType", type.Name);
            Assert.Equal(typeof(IFoo), type.GetInterface(nameof(IFoo)));
        }

        [Fact(DisplayName = "Emit a dynamic type at runtime")]
        public void EmitDynamicTypeAtRuntime()
        {
            // Given
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(ITypeEmitter).Assembly);
            var asmEmitter = iocContainer.GetInstance<IAssemblyEmitter>();

            // When
            var emitter = asmEmitter.EmitType("My.Namespace.MyType");
            var type = emitter.ToType();

            // Then
            Assert.NotNull(emitter);
            Assert.NotNull(type);
            Assert.Equal("My.Namespace", type.Namespace);
            Assert.Equal("MyType", type.Name);
        }
    }
}