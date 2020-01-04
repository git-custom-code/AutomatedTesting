namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="AssemblyEmitter"/> type.
    /// </summary>
    public sealed class AssemblyEmitterTests
    {
        [Fact(DisplayName = "Create a new dynamic type at runtime.")]
        public void EmitDynamicTypeAtRuntime()
        {
            // Given
            var emitter = new AssemblyEmitter();

            // When
            var type = emitter.EmitType("My.Namespace.MyType");

            // Then
            Assert.NotNull(type);
            Assert.Equal("My.Namespace", type.Namespace);
            Assert.Equal("MyType", type.Name);
        }
    }
}