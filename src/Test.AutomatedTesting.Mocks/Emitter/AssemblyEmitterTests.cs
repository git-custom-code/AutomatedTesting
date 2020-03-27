namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using System;
    using System.Reflection.Emit;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="AssemblyEmitter"/> type.
    /// </summary>
    public sealed class AssemblyEmitterTests
    {
        [Fact(DisplayName = "Create a new dynamic in-memory assembly at runtime")]
        public void EmitDynamicAssemlyAtRuntime()
        {
            // Given
            var emitter = new AssemblyEmitter(builder => new MockEmitter(builder));

            // When
            var typeEmitter = emitter.EmitType("My.Namespace.MyType");

            // Then
            Assert.NotNull(typeEmitter);
            Assert.IsType<MockEmitter>(typeEmitter);
        }

        #region Mocks

        private sealed class MockEmitter : ITypeEmitter
        {
            public MockEmitter(TypeBuilder builder)
            {
                Builder = builder;
            }

            public TypeBuilder Builder { get; }

            public void ImplementDecorator<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public void ImplementDecorator(Type signature)
            {
                throw new NotImplementedException();
            }

            public void ImplementInterface<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public void ImplementInterface(Type @interface)
            {
                throw new NotImplementedException();
            }

            public Type ToType()
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}