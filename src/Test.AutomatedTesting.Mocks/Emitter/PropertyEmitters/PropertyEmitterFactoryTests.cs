namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using CustomCode.AutomatedTesting.Mocks.Interception;
    using ExceptionHandling;
    using System.Reflection;
    using System.Reflection.Emit;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="PropertyEmitterFactory"/> type.
    /// </summary>
    public sealed class PropertyEmitterFactoryTests
    {
        [Fact(DisplayName = "Create property emitter for a getter only property")]
        public void CreateEmitterForPropertyGetter()
        {
            // Given
            var name = new AssemblyName("DynamicMockAssembly");
            var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            var module = assembly.DefineDynamicModule("DynamicMockModule");

            var typeBuilder = module.DefineType("DynamicType");
            var interceptor = typeBuilder.DefineField("_interceptor", typeof(IInterceptor), FieldAttributes.Private);
            var type = typeof(IFooValueTypeGetter<int>);
            var propertyName = nameof(IFooValueTypeGetter<int>.Getter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterEmitter<int>>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a setter only property")]
        public void CreateEmitterForPropertySetter()
        {
            // Given
            var name = new AssemblyName("DynamicMockAssembly");
            var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            var module = assembly.DefineDynamicModule("DynamicMockModule");

            var typeBuilder = module.DefineType("DynamicType");
            var interceptor = typeBuilder.DefineField("_interceptor", typeof(IInterceptor), FieldAttributes.Private);
            var type = typeof(IFooValueTypeSetter<int>);
            var propertyName = nameof(IFooValueTypeSetter<int>.Setter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptSetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a getter/setter property")]
        public void CreateEmitterForPropertyGetterSetter()
        {
            // Given
            var name = new AssemblyName("DynamicMockAssembly");
            var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            var module = assembly.DefineDynamicModule("DynamicMockModule");

            var typeBuilder = module.DefineType("DynamicType");
            var interceptor = typeBuilder.DefineField("_interceptor", typeof(IInterceptor), FieldAttributes.Private);
            var type = typeof(IFooValueTypeProperty<int>);
            var propertyName = nameof(IFooValueTypeProperty<int>.GetterSetter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterSetterEmitter<int>>(emitter);
        }
    }
}