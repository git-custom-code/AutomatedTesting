namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;

/// <summary>
/// Test domain implementation of the <see cref="IFooFuncValueTypeParameterRef{T}"/> interface.
/// </summary>
public sealed class FooFuncValueTypeParameterRef<T> : IFooFuncValueTypeParameterRef<T>
    where T : struct
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="FooFuncValueTypeParameterRef{T}"/> type.
    /// </summary>
    /// <param name="value"> The method's result value. </param>
    public FooFuncValueTypeParameterRef(T value)
    {
        Value = value;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithOneParameter(ref T)"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    /// <summary>
    /// Gets the passed parameter values.
    /// </summary>
    public IList<T> Parameters { get; } = new List<T>();

    /// <summary>
    /// Gets the method's result value.
    /// </summary>
    private T Value { get; }

    #endregion

    #region Logic

    /// <inheritdoc />
    public T MethodWithOneParameter(ref T first)
    {
        CallCount++;
        Parameters.Add(first);
        first = default;
        return Value;
    }

    #endregion
}
