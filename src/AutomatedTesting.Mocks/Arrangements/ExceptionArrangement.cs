namespace CustomCode.AutomatedTesting.Mocks.Arrangements;

using ExceptionHandling;
using Interception;
using System;
using System.Reflection;

/// <summary>
/// Arrange that an intercepted method or property call will throw an exception.
/// </summary>
public sealed class ExceptionArrangement : IArrangement
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ExceptionArrangement"/> type.
    /// </summary>
    /// <param name="signature">
    /// The signature of the intercepted method or property that is the target for this arrangement.
    /// </param>
    /// <param name="exceptionFactory"> A factory that will create the exception instance to be thrown. </param>
    public ExceptionArrangement(MethodInfo signature, Func<Exception> exceptionFactory)
    {
        Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        ExceptionFactory = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets a factory that will create the exception instance to be thrown.
    /// </summary>
    private Func<Exception> ExceptionFactory { get; }

    /// <summary>
    /// Gets the signature of the intercepted method or property that is the target for this arrangement.
    /// </summary>
    private MethodInfo Signature { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IArrangement" />
    public void ApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        TryApplyTo(invocation);
    }

    /// <inheritdoc cref="IArrangement" />
    public bool CanApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        return invocation.Signature == Signature;
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        var exception = ExceptionFactory();
        return $"Calls to '{Signature.Name}' should throw an '{exception.GetType().Name}'";
    }

    /// <inheritdoc cref="IArrangement" />
    public bool TryApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.Signature == Signature)
        {
            throw ExceptionFactory();
        }

        return false;
    }

    #endregion
}
