namespace CustomCode.AutomatedTesting.Mocks.Interception;

using Arrangements;
using System;

/// <summary>
/// Implementation of the <see cref="IInterceptor"/> interface for mocked dependency instances that will return
/// default values for each intercepted method and property.
/// </summary>
public sealed class LooseMockInterceptor : IInterceptor
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="LooseMockInterceptor"/> type.
    /// </summary>
    /// <param name="arrangements"> A collection of <see cref="IArrangement"/>s for the intercepted calls. </param>
    public LooseMockInterceptor(IArrangementCollection arrangements)
    {
        Arrangements = arrangements ?? throw new ArgumentNullException(nameof(arrangements));
    }

    /// <summary>
    /// Gets a collection of <see cref="IArrangement"/>s for the intercepted calls.
    /// </summary>
    private IArrangementCollection Arrangements { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IInterceptor" />
    public bool Intercept(IInvocation invocation)
    {
        Arrangements.ApplyTo(invocation);
        return true;
    }

    #endregion
}
