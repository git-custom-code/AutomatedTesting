namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

using CustomCode.AutomatedTesting.Mocks.Interception;
using ExceptionHandling;
using System.Reflection;
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
        var (property, type, interceptor, decoratee) = CreateParameter<IFooValueTypeGetter<int>>(
            propertyName: nameof(IFooValueTypeGetter<int>.Getter));
        var factory = new PropertyDecoratorEmitterFactory();

        // When
        var emitter = factory.CreatePropertyEmitterFor(property, type, decoratee, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<DecorateGetterEmitter<int>>(emitter);
    }

    [Fact(DisplayName = "Create property decorator emitter for a setter only property")]
    public void CreateEmitterForPropertySetter()
    {
        // Given
        var (property, type, interceptor, decoratee) = CreateParameter<IFooValueTypeSetter<int>>(
            propertyName: nameof(IFooValueTypeSetter<int>.Setter));
        var factory = new PropertyDecoratorEmitterFactory();

        // When
        var emitter = factory.CreatePropertyEmitterFor(property, type, decoratee, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<DecorateSetterEmitter>(emitter);
    }

    [Fact(DisplayName = "Create property decorator emitter for a getter/setter property")]
    public void CreateEmitterForPropertyGetterSetter()
    {
        // Given
        var (property, type, interceptor, decoratee) = CreateParameter<IFooValueTypeProperty<int>>(
            propertyName: nameof(IFooValueTypeProperty<int>.GetterSetter));
        var factory = new PropertyDecoratorEmitterFactory();

        // When
        var emitter = factory.CreatePropertyEmitterFor(property, type, decoratee, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<DecorateGetterSetterEmitter<int>>(emitter);
    }

    #region Mocks

    private (PropertyInfo property, TypeBuilder type, FieldBuilder interceptor, FieldBuilder decoratee)
        CreateParameter<T>(string propertyName)
    {
        var name = new AssemblyName("DynamicMockAssembly");
        var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
        var module = assembly.DefineDynamicModule("DynamicMockModule");

        var typeBuilder = module.DefineType("DynamicType");
        var interceptor = typeBuilder.DefineField("_interceptor", typeof(IInterceptor), FieldAttributes.Private);
        var decoratee = typeBuilder.DefineField("_decoratee", typeof(T), FieldAttributes.Private);

        var type = typeof(T);
        var property = type.GetProperty(propertyName) ?? throw new PropertyInfoException(type, propertyName);
        return (property, typeBuilder, interceptor, decoratee);
    }

    #endregion
}
