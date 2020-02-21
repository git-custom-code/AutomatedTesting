namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using System.Reflection.Emit;
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
            TypeBuilder? type = null;
            FieldBuilder? interceptor = null;
            var property = typeof(IFoo).GetProperty(nameof(IFoo.Getter));
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, type, interceptor);
#pragma warning restore CS8604 // Possible null reference argument.

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a setter only property")]
        public void CreateEmitterForPropertySetter()
        {
            // Given
            TypeBuilder? type = null;
            FieldBuilder? interceptor = null;
            var property = typeof(IFoo).GetProperty(nameof(IFoo.Setter));
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, type, interceptor);
#pragma warning restore CS8604 // Possible null reference argument.

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptSetterEmitter>(emitter);
        }

        [Fact(DisplayName = "Create property emitter for a getter/setter property")]
        public void CreateEmitterForPropertyGetterSetter()
        {
            // Given
            TypeBuilder? type = null;
            FieldBuilder? interceptor = null;
            var property = typeof(IFoo).GetProperty(nameof(IFoo.GetterSetter));
            var factory = new PropertyEmitterFactory();

            // When
#pragma warning disable CS8604 // Possible null reference argument.
            var emitter = factory.CreatePropertyEmitterFor(property, type, interceptor);
#pragma warning restore CS8604 // Possible null reference argument.

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptGetterSetterEmitter>(emitter);
        }

        #region Domain

        public interface IFoo
        {
            int Getter { get; }

            int Setter { set; }

            int GetterSetter { get; set; }
        }

        #endregion
    }
}