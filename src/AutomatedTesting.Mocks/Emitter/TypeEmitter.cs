namespace CustomCode.AutomatedTesting.Mocks.Emitter;

using ExceptionHandling;
using System;
using System.Linq;
using System.Reflection.Emit;

/// <summary>
/// Default implementation of the <see cref="ITypeEmitter"/> interface.
/// </summary>
public sealed class TypeEmitter : ITypeEmitter
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="TypeEmitter"/> type.
    /// </summary>
    /// <param name="typeBuilder"> The internal <see cref="TypeBuilder"/> that is used to dynamically emit logic. </param>
    /// <param name="dependencyEmitter"> The emitter instance used for creating the type's dependencies. </param>
    /// <param name="methodEmitterFactory">
    /// A factory that can create <see cref="IMethodEmitter"/> instance based on a method's signature.
    /// </param>
    /// <param name="propertyEmitterFactory">
    /// A factory that can create <see cref="IPropertyEmitter"/> instance based on a property's signature.
    /// </param>
    public TypeEmitter(
        TypeBuilder typeBuilder,
        IDependencyEmitter dependencyEmitter,
        IMethodEmitterFactory methodEmitterFactory,
        IPropertyEmitterFactory propertyEmitterFactory)
    {
        Type = typeBuilder ?? throw new ArgumentNullException(nameof(typeBuilder));
        Dependencies = dependencyEmitter ?? throw new ArgumentNullException(nameof(dependencyEmitter));
        MethodEmitterFactory = methodEmitterFactory ?? throw new ArgumentNullException(nameof(methodEmitterFactory));
        PropertyEmitterFactory = propertyEmitterFactory ?? throw new ArgumentNullException(nameof(propertyEmitterFactory));
    }

    /// <summary>
    /// Gets the emitter instance used for creating the type's dependencies.
    /// </summary>
    private IDependencyEmitter Dependencies { get; }

    /// <summary>
    /// Get a factory that can create <see cref="IMethodEmitter"/> instance based on a method's signature.
    /// </summary>
    private IMethodEmitterFactory MethodEmitterFactory { get; }

    /// <summary>
    /// Get a factory that can create <see cref="IPropertyEmitter"/> instance based on a property's signature.
    /// </summary>
    private IPropertyEmitterFactory PropertyEmitterFactory { get; }

    /// <summary>
    /// Gets the internal <see cref="TypeBuilder"/> that is used to dynamically emit logic.
    /// </summary>
    private TypeBuilder Type { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="ITypeEmitter" />
    public void ImplementInterface<T>() where T : class
    {
        var @interface = typeof(T);
        ImplementInterface(@interface);
    }

    /// <inheritdoc cref="ITypeEmitter" />
    public void ImplementInterface(Type signature)
    {
        Ensures.NotNull(signature);
        Ensures.IsInterface(signature);

        var interceptorField = Dependencies.CreateInterceptorDependency(Type);
        Dependencies.CreateConstructor(Type, interceptorField);
        Type.AddInterfaceImplementation(signature);

        foreach (var property in signature.GetProperties())
        {
            var emitter = PropertyEmitterFactory.CreatePropertyEmitterFor(property, Type, interceptorField);
            emitter.EmitPropertyImplementation();
        }

        foreach (var method in signature.GetMethods().Where(m => !m.IsSpecialName))
        {
            var emitter = MethodEmitterFactory.CreateMethodEmitterFor(method, Type, interceptorField);
            emitter.EmitMethodImplementation();
        }
    }

    /// <inheritdoc cref="ITypeEmitter" />
    public Type ToType()
    {
        try
        {
            var type = Type.CreateType();
            if (type == null)
            {
                throw new Exception($"Unable to creat a proxy for \"{Type.Name}\"");
            }
            return type;
        }
        catch (Exception e)
        {
            throw new Exception($"Unable to creat a proxy for \"{Type.Name}\"", e);
        }
    }

    #endregion
}
