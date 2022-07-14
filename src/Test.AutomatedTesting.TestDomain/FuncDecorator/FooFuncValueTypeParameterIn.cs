namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;

/// <summary>
/// Test domain implementation of the <see cref="IFooFuncValueTypeParameterIn{T}"/> interface.
/// </summary>
public sealed class FooFuncValueTypeParameterIn<T> : IFooFuncValueTypeParameterIn<T>
    where T : struct
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="FooFuncValueTypeParameterIn{T}"/> type.
    /// </summary>
    /// <param name="value"> The method's result value. </param>
    public FooFuncValueTypeParameterIn(T value)
    {
        Value = value;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithOneParameter(T)"/> was called.
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
    public T MethodWithOneParameter(T first)
    {
        CallCount++;
        Parameters.Add(first);
        return Value;
    }

    #endregion
}
