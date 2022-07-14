namespace CustomCode.AutomatedTesting.Mocks.Core.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

/// <summary>
/// Can be used in combination with a <see cref="ClassDataAttribute"/> for theories (see <see cref="TheoryAttribute"/>)
/// that test different reference types (supplies two parameters).
/// </summary>
public sealed class TwoParameterReferenceTypeData : IEnumerable<object?[]>
{
    #region Data

    /// <summary>
    /// Gets a collection of data for different c# value types.
    /// </summary>
    private IEnumerable<object?[]> Data { get; } = new List<object?[]>(new[] {
            new object?[] { (string)"foo", (string)"bar" },
            new object?[] { (object?)null, (object?)null },
            new object?[] { (Exception)new SerializableException("Foo"), (Exception)new SerializableException("Bar") }
        });

    #endregion

    #region Logic

    /// <inheritdoc />
    public IEnumerator<object?[]> GetEnumerator()
    {
        foreach(var data in Data)
        {
            yield return data;
        }
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}
