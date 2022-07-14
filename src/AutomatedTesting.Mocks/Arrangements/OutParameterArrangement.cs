namespace CustomCode.AutomatedTesting.Mocks.Arrangements;

using ExceptionHandling;
using Interception;
using Interception.Parameters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

/// <summary>
/// Arrange a custom out parameter value for an intercepted method call.
/// </summary>
/// <typeparam name="T"> The type of the arranged out parameter value. </typeparam>
public sealed class OutParameterArrangement<T> : IArrangement
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="OutParameterArrangement{T}"/> type.
    /// </summary>
    /// <param name="signature">
    /// The signature of the intercepted method that is the target for this arrangement.
    /// </param>
    /// <param name="outParameterName"> The name of the out parameter. </param>
    /// <param name="outParameterValue"> The arranged out parameter value. </param>
    public OutParameterArrangement(MethodInfo signature, string outParameterName, [MaybeNull] T outParameterValue)
    {
        Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        OutParameterName = outParameterName ?? throw new ArgumentNullException(nameof(outParameterName));
        OutParameterValue = outParameterValue;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the name of the out parameter.
    /// </summary>
    private string OutParameterName { get; }

    /// <summary>
    /// Gets the arranged out parameter value.
    /// </summary>
    [MaybeNull]
    private T OutParameterValue { get; }

    /// <summary>
    /// Gets the signature of the intercepted method that is the target for this arrangement.
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

        if (invocation.TryGetFeature<IParameterOut>(out var outParameterFeature))
        {
            if (invocation.Signature == Signature)
            {
                return outParameterFeature
                    .OutParameterCollection
                    .Any(p => p.Name == OutParameterName && p.Type == typeof(T));
            }
        }

        return false;
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        return $"Calls to '{Signature.Name}' should return '{OutParameterValue}' for out parameter '{OutParameterName}'";
    }

    /// <inheritdoc cref="IArrangement" />
    public bool TryApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.Signature == Signature)
        {
            if (invocation.TryGetFeature<IParameterOut>(out var outParameterFeature))
            {
                var parameter = outParameterFeature
                    .OutParameterCollection
                    .SingleOrDefault(p => p.Name == OutParameterName && p.Type == typeof(T));
                if (parameter != null)
                {
                    parameter.Value = OutParameterValue;
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}
