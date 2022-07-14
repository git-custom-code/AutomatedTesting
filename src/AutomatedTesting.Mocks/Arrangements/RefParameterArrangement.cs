namespace CustomCode.AutomatedTesting.Mocks.Arrangements;

using ExceptionHandling;
using Interception;
using Interception.Parameters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

/// <summary>
/// Arrange a custom ref parameter value for an intercepted method call.
/// </summary>
/// <typeparam name="T"> The type of the arranged ref parameter value. </typeparam>
public sealed class RefParameterArrangement<T> : IArrangement
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="RefParameterArrangement{T}"/> type.
    /// </summary>
    /// <param name="signature">
    /// The signature of the intercepted method that is the target for this arrangement.
    /// </param>
    /// <param name="refParameterName"> The name of the ref parameter. </param>
    /// <param name="refParameterValue"> The arranged ref parameter value. </param>
    public RefParameterArrangement(MethodInfo signature, string refParameterName, [MaybeNull] T refParameterValue)
    {
        Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        RefParameterName = refParameterName ?? throw new ArgumentNullException(nameof(refParameterName));
        RefParameterValue = refParameterValue;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the name of the ref parameter.
    /// </summary>
    private string RefParameterName { get; }

    /// <summary>
    /// Gets the arranged ref parameter value.
    /// </summary>
    [MaybeNull]
    private T RefParameterValue { get; }

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

        if (invocation.TryGetFeature<IParameterRef>(out var refParameterFeature))
        {
            if (invocation.Signature == Signature)
            {
                return refParameterFeature
                    .RefParameterCollection
                    .Any(p => p.Name == RefParameterName && p.Type == typeof(T));
            }
        }

        return false;
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        return $"Calls to '{Signature.Name}' should return '{RefParameterValue}' for ref parameter '{RefParameterName}'";
    }

    /// <inheritdoc cref="IArrangement" />
    public bool TryApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.Signature == Signature)
        {
            if (invocation.TryGetFeature<IParameterRef>(out var refParameterFeature))
            {
                var parameter = refParameterFeature
                    .RefParameterCollection
                    .SingleOrDefault(p => p.Name == RefParameterName && p.Type == typeof(T));
                if (parameter != null)
                {
                    parameter.Value = RefParameterValue;
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}
