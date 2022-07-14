namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Test domain implementation of the <see cref="IFooGenericValueTaskValueTypeParameter"/> interface.
/// </summary>
public sealed class FooGenericValueTaskValueTypeParameter : IFooGenericValueTaskValueTypeParameter
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="FooGenericValueTaskValueTypeParameter"/> type.
    /// </summary>
    /// <param name="value"> The method's return value. </param>
    public FooGenericValueTaskValueTypeParameter(int value)
    {
        Value = value;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithOneParameterAsync(int)"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    /// <summary>
    /// Gets the method's parameter.
    /// </summary>
    public int Parameter { get; private set; }

    /// <summary>
    /// Gets the method's return value.
    /// </summary>
    private int Value { get; }

    #endregion

    #region Logic

    /// <inheritdoc />
    public ValueTask<int> MethodWithOneParameterAsync(int first)
    {
        CallCount++;
        Parameter = first;
        return new ValueTask<int>(Value);
    }

    #endregion
}
