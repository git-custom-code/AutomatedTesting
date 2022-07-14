namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Test domain implementation of the <see cref="IFooValueTaskParameterless"/> interface.
/// </summary>
public sealed class FooValueTaskParameterless : IFooValueTaskParameterless
{
    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithoutParameterAsync"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    #endregion

    #region Logic

    /// <inheritdoc />
    public ValueTask MethodWithoutParameterAsync()
    {
        CallCount++;
        return new ValueTask();
    }

    #endregion
}
