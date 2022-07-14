namespace CustomCode.AutomatedTesting.Mocks;

using Interception;

/// <summary>
/// Interface for types that contain arrangements for intercepted method or property invocations
/// (e.g. a return value, custom logic, ...).
/// </summary>
public interface IArrangement
{
    /// <summary>
    /// Applies the arrangement to an intercepted <paramref name="invocation"/> if the signatures match.
    /// </summary>
    /// <param name="invocation"> The intercepted method or property invocation. </param>
    void ApplyTo(IInvocation invocation);

    /// <summary>
    /// Query if the signature of the arrangment matches with the signature of the <paramref name="invocation"/>.
    /// </summary>
    /// <param name="invocation"> The target incovation. </param>
    /// <returns> True if the arrangment can be applied to the incovation, false otherwise. </returns>
    bool CanApplyTo(IInvocation invocation);

    /// <summary>
    /// Try to apply the arrangement to an intercepted <paramref name="invocation"/> if the signatures match.
    /// </summary>
    /// <param name="invocation"> The intercepted method or property invocation. </param>
    /// <returns> True if the arrangment was applied to the incovation, false otherwise. </returns>
    bool TryApplyTo(IInvocation invocation);
}
