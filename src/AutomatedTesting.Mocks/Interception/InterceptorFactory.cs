namespace CustomCode.AutomatedTesting.Mocks.Interception;

using Arrangements;
using ExceptionHandling;
using System;

/// <summary>
/// Default implementation of the <see cref="IInterceptorFactory"/> interface.
/// </summary>
public sealed class InterceptorFactory : IInterceptorFactory
{
    #region Logic

    /// <inheritdoc cref="IInterceptorFactory" />
    public IInterceptor CreateInterceptorFor(MockBehavior behavior, IArrangementCollection arrangements)
    {
        Ensures.NotNull(arrangements);

        if (behavior == MockBehavior.Loose)
        {
            return new LooseMockInterceptor(arrangements);
        }

        if (behavior == MockBehavior.Strict)
        {
            return new StrictMockInterceptor(arrangements);
        }

        if (behavior == MockBehavior.Partial)
        {
            return new PartialMockInterceptor(arrangements);
        }

        throw new NotSupportedException();
    }

    #endregion
}
