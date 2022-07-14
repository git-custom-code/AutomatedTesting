namespace CustomCode.AutomatedTesting.Mocks.Emitter;

using Interception;
using System;
using System.Reflection;
using System.Reflection.Emit;

/// <summary>
/// Abstract base type for <see cref="IMethodEmitter"/> interface implementations that defines
/// a set of common functionality that can be used by the specialized implementations.
/// </summary>
public abstract partial class MethodEmitterBase : IMethodEmitter
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MethodEmitterBase"/> type.
    /// </summary>
    /// <param name="type"> The dynamic proxy type. </param>
    /// <param name="signature"> The signature of the method to be created. </param>
    /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
    protected MethodEmitterBase(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        InterceptorField = interceptorField ?? throw new ArgumentNullException(nameof(interceptorField));
    }

    /// <summary>
    /// Gets The <see cref="Type"/>'s <see cref="IInterceptor"/> backing field.
    /// </summary>
    protected FieldBuilder InterceptorField { get; }

    /// <summary>
    /// Gets the signature of the method to be created.
    /// </summary>
    protected MethodInfo Signature { get; }

    /// <summary>
    /// Gets the dynamic proxy type.
    /// </summary>
    protected TypeBuilder Type { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IMethodEmitter" />
    public abstract void EmitMethodImplementation();

    #endregion
}
