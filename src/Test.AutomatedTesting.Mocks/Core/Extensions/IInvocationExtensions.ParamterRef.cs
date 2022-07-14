namespace CustomCode.AutomatedTesting.Mocks.Core.Extensions;

using Interception;
using Interception.Parameters;
using System;
using System.Linq;
using Xunit;

/// <summary>
/// Extension methods for the <see cref="IInvocation"/> type.
/// </summary>
public static partial class IInvocationExtensions
{
    #region Logic

    /// <summary>
    /// Validates that the invocation has no ref parameters.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    public static void ShouldHaveNoParameterRef(this IInvocation invocation)
    {
        Assert.False(invocation.HasFeature<IParameterRef>());
    }

    /// <summary>
    /// Validates that the invocation has <paramref name="count"/> ref parameters.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    /// <param name="count"> The number of ref parameter to be validated. </param>
    public static void ShouldHaveParameterRefCountOf(this IInvocation invocation, int count)
    {
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Equal(count, feature.RefParameterCollection.Count());
    }

    /// <summary>
    /// Validates that the invocation has a ref parameter with the given name, type and value.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    /// <param name="name"> The unique name of the ref parameter to be validated. </param>
    /// <param name="type"> The type of the ref parameter to be validated. </param>
    /// <param name="value"> The value of the ref parameter to be validated. </param>
    public static void ShouldHaveParameterRef(this IInvocation invocation, string name, Type type, object? value)
    {
        var feature = invocation.GetFeature<IParameterRef>();
        var parameter = feature.RefParameterCollection.SingleOrDefault(
            p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        Assert.Equal(name, parameter.Name);
        Assert.Equal(type, parameter.Type);
        Assert.Equal(value, parameter.Value);
    }

    #endregion
}
