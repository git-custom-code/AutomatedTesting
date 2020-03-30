namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using ExceptionHandling;
    using System.Reflection.Emit;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="PropertyDecoratorEmitterFactory"/> type.
    /// </summary>
    public sealed class PropertyDecoratorEmitterFactoryTests
    {
        [Fact(DisplayName = "Create property decorator emitter for a getter only property")]
        public void CreateEmitterForPropertyGetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? decoratee = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeGetter<int>);
            var propertyName = nameof(IFooValueTypeGetter<int>.Getter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyDecoratorEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, decoratee, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<DecorateGetterEmitter<int>>(emitter);
        }

        [Fact(DisplayName = "Create property decorator emitter for a setter only property")]
        public void CreateEmitterForPropertySetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? decoratee = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeSetter<int>);
            var propertyName = nameof(IFooValueTypeSetter<int>.Setter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyDecoratorEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, decoratee, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<DecorateSetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property decorator emitter for a getter/setter property")]
        public void CreateEmitterForPropertyGetterSetter()
        {
            // Given
            TypeBuilder? typeBuilder = null;
            FieldBuilder? decoratee = null;
            FieldBuilder? interceptor = null;
            var type = typeof(IFooValueTypeProperty<int>);
            var propertyName = nameof(IFooValueTypeProperty<int>.GetterSetter);
            var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
            var factory = new PropertyDecoratorEmitterFactory();

            // When
#nullable disable
            var emitter = factory.CreatePropertyEmitterFor(property, typeBuilder, decoratee, interceptor);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<DecorateGetterSetterEmitter<int>>(emitter);
        }
    }
}