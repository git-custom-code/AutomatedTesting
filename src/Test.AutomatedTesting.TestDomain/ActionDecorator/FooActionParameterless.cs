namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Test domain implementation of the <see cref="IFooActionParameterless"/> interface.
/// </summary>
public sealed class FooActionParameterless : IFooActionParameterless
{
    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithoutParameter"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    #endregion

    #region Logic

    /// <inheritdoc />
    public void MethodWithoutParameter()
    {
        CallCount++;
    }

    #endregion
}
