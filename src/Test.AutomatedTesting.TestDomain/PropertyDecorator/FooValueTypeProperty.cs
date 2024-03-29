namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Test domain implementation of the <see cref="IFooValueTypeProperty{T}"/> interface.
/// </summary>
public sealed class FooValueTypeProperty<T> : IFooValueTypeProperty<T>
    where T : struct
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="FooValueTypeProperty{T}"/> type.
    /// </summary>
    /// <param name="returnValue"> The <see cref="GetterSetter"/>'s return value. </param>
    public FooValueTypeProperty(T returnValue)
    {
        ReturnValue = returnValue;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the <see cref="GetterSetter"/>'s return value.
    /// </summary>
    private T ReturnValue { get; }

    /// <summary>
    /// Gets the <see cref="GetterSetter"/>'s set value.
    /// </summary>
    public T Value { get; private set; }

    /// <summary>
    /// Gets the number of times the <see cref="GetterSetter"/>'s getter was called.
    /// </summary>
    public uint GetterCallCount { get; private set; } = 0;

    /// <summary>
    /// Gets the number of times the <see cref="GetterSetter"/>'s setter was called.
    /// </summary>
    public uint SetterCallCount { get; private set; } = 0;

    #endregion

    #region Data

    /// <inheritdoc />
    public T GetterSetter
    {
        get
        {
            GetterCallCount++;
            return ReturnValue;
        }
        set
        {
            SetterCallCount++;
            Value = value;
        }
    }

    #endregion
}
