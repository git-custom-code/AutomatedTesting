namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Test domain implementation of the <see cref="IFooAsyncEnumerableReferenceTypeParameter"/> interface.
/// </summary>
public sealed class FooAsyncEnumerableReferenceTypeParameter : IFooAsyncEnumerableReferenceTypeParameter
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="FooAsyncEnumerableReferenceTypeParameter"/> type.
    /// </summary>
    /// <param name="result"> The method's return value. </param>
    public FooAsyncEnumerableReferenceTypeParameter(IEnumerable<object?> result)
    {
        Result = result.ToAsyncEnumerable();
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithOneParameterAsync(object?)"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    /// <summary>
    /// Gets the method's parameter.
    /// </summary>
    public object? Parameter { get; private set; }

    /// <summary>
    /// Gets the method's return value.
    /// </summary>
    private IAsyncEnumerable<object?> Result { get; }

    #endregion

    #region Logic

    /// <inheritdoc />
    public IAsyncEnumerable<object?> MethodWithOneParameterAsync(object? first)
    {
        CallCount++;
        Parameter = first;
        return Result;
    }

    #endregion
}
