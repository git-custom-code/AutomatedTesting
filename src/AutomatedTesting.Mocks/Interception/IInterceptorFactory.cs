namespace CustomCode.AutomatedTesting.Mocks.Interception;

using Arrangements;

/// <summary>
/// Interface that defines a factory for creating a matching <see cref="IInterceptor"/> instance
/// for a given <see cref="MockBehavior"/>.
/// </summary>
public interface IInterceptorFactory
{
    /// <summary>
    /// Create a new <see cref="IInterceptor"/> for a given <paramref name="behavior"/>.
    /// </summary>
    /// <param name="behavior"> The desired behavior of the interceptor. </param>
    /// <param name="arrangements">
    /// A collection of user made arrangements for intercepted method or property calls.
    /// </param>
    /// <returns> The newly created interceptor instance. </returns>
    IInterceptor CreateInterceptorFor(MockBehavior behavior, IArrangementCollection arrangements);
}
