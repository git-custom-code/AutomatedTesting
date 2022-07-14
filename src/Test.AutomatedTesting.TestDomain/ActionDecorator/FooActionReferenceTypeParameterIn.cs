namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;

/// <summary>
/// Test domain implementation of the <see cref="IFooActionReferenceTypeParameterIn{T}"/> interface.
/// </summary>
/// <typeparam name="T"> The value type of the method's parameters. </typeparam>
public sealed class FooActionReferenceTypeParameterIn<T> : IFooActionReferenceTypeParameterIn<T>
    where T : class
{
    #region Data

    /// <summary>
    /// Gets the number of times the <see cref="MethodWithOneParameter(T?)"/> was called.
    /// </summary>
    public uint CallCount { get; private set; } = 0;

    /// <summary>
    /// Gets the passed parameter values.
    /// </summary>
    public IList<T?> Parameters { get; } = new List<T?>();

    #endregion

    #region Logic

    /// <inheritdoc />
    public void MethodWithOneParameter(T? first)
    {
        CallCount++;
        Parameters.Add(first);
    }

    #endregion
}
