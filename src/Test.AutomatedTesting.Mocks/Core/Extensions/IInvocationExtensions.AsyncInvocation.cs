namespace CustomCode.AutomatedTesting.Mocks.Core.Extensions;

using Interception;
using Interception.Async;
using Xunit;

/// <summary>
/// Extension methods for the <see cref="IInvocation"/> type.
/// </summary>
public static partial class IInvocationExtensions
{
    #region Logic

    /// <summary>
    /// Validates that the invocation is of the given asynchronous <paramref name="type"/>.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    /// <param name="type"> The expected <see cref="AsyncInvocationType"/>. </param>
    public static void ShouldBeAsyncInvocationOfType(this IInvocation invocation, AsyncInvocationType type)
    {
        var feature = invocation.GetFeature<IAsyncInvocation>();
        Assert.Equal(type, feature.Type);
    }

    #endregion
}
