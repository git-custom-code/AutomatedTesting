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
            var type = typeof(IFooWithValueTypeProperties);
            var propertyName = nameof(IFooWithValueTypeProperties.Getter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#pragma warning restore CS8604

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a setter only property")]
        public void CreateEmitterForPropertySetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooWithValueTypeProperties);
            var propertyName = nameof(IFooWithValueTypeProperties.Setter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#pragma warning restore CS8604

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
            var type = typeof(IFooWithValueTypeProperties);
            var propertyName = nameof(IFooWithValueTypeProperties.GetterSetter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, interceptor);
#pragma warning restore CS8604

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterSetterEmitter>(emitter);
        }
    }
}