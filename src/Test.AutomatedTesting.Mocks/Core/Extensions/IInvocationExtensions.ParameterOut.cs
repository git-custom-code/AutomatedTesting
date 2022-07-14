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
    /// Validates that the invocation has no out parameters.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    public static void ShouldHaveNoParameterOut(this IInvocation invocation)
    {
        Assert.False(invocation.HasFeature<IParameterOut>());
    }

    /// <summary>
    /// Validates that the invocation has <paramref name="count"/> out parameters.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    /// <param name="count"> The number of out parameter to be validated. </param>
    public static void ShouldHaveParameterOutCountOf(this IInvocation invocation, int count)
    {
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Equal(count, feature.OutParameterCollection.Count());
    }

    /// <summary>
    /// Validates that the invocation has a out parameter with the given name, type and value.
    /// </summary>
    /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
    /// <param name="name"> The unique name of the out parameter to be validated. </param>
    /// <param name="type"> The type of the out parameter to be validated. </param>
    /// <param name="value"> The value of the out parameter to be validated. </param>
    public static void ShouldHaveParameterOut(this IInvocation invocation, string name, Type type, object? value)
    {
        var feature = invocation.GetFeature<IParameterOut>();
        var parameter = feature.OutParameterCollection.SingleOrDefault(
            p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        Assert.Equal(name, parameter.Name);
        Assert.Equal(type, parameter.Type);
        Assert.Equal(value, parameter.Value);
    }

    #endregion
}
