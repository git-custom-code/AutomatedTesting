namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Test domain implementation of the <see cref="IFooReferenceTypeSetter{T}"/> interface.
/// </summary>
public sealed class FooReferenceTypeSetter<T> : IFooReferenceTypeSetter<T>
    where T : class
{
    #region Data

    /// <summary>
    /// Gets the <see cref="Setter"/>'s value.
    /// </summary>
    public T? Value { get; private set; }

    /// <summary>
    /// Gets the number of times the <see cref="Setter"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    #endregion

    #region Data

    /// <inheritdoc />
    public T? Setter
    {
        set
        {
            CallCount++;
            Value = value;
        }
    }

    #endregion
}
