namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

using System;
using System.Reflection.Emit;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="AssemblyEmitter"/> type.
/// </summary>
public sealed class AssemblyEmitterTests
{
    [Fact(DisplayName = "AssemblyEmitter: Emit type")]
    public void EmitDynamicProxy()
    {
        // Given
        var emitter = new AssemblyEmitter(
            builder => new MockEmitter(builder),
            builder => new MockDecoratorEmitter(builder));

        // When
        var typeEmitter = emitter.EmitType("My.Namespace.MyType");

        // Then
        Assert.NotNull(typeEmitter);
        Assert.IsType<MockEmitter>(typeEmitter);
    }

    [Fact(DisplayName = "AssemblyEmitter: Emit decorator type")]
    public void EmitDynamicDecorator()
    {
        // Given
        var emitter = new AssemblyEmitter(
            builder => new MockEmitter(builder),
            builder => new MockDecoratorEmitter(builder));

        // When
        var typeEmitter = emitter.EmitDecoratorType("My.Namespace.MyType");

        // Then
        Assert.NotNull(typeEmitter);
        Assert.IsType<MockDecoratorEmitter>(typeEmitter);
    }

    #region Mocks

    private sealed class MockEmitter : ITypeEmitter
    {
        public MockEmitter(TypeBuilder builder)
        {
            Builder = builder;
        }

        public TypeBuilder Builder { get; }

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

    private sealed class MockDecoratorEmitter : ITypeDecoratorEmitter
    {
        public MockDecoratorEmitter(TypeBuilder builder)
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

        public Type ToType()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
