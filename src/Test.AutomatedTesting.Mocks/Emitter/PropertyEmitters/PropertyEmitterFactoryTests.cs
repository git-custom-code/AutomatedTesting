namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using ExceptionHandling;
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
            TypeBuilder? typeBuilder = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeGetter<int>);
            var propertyName = nameof(IFooValueTypeGetter<int>.Getter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterEmitter<int>>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a setter only property")]
        public void CreateEmitterForPropertySetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeSetter<int>);
            var propertyName = nameof(IFooValueTypeSetter<int>.Setter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptSetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a getter/setter property")]
        public void CreateEmitterForPropertyGetterSetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeProperty<int>);
            var propertyName = nameof(IFooValueTypeProperty<int>.GetterSetter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterSetterEmitter<int>>(emitter);
        }
    }
}