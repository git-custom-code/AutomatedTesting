namespace CustomCode.AutomatedTesting.Mocks.Core.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using TestDomain;
using Xunit;

/// <summary>
/// Can be used in combination with a <see cref="ClassDataAttribute"/> for theories (see <see cref="TheoryAttribute"/>)
/// that test different reference types (supplies one parameter).
/// </summary>
public sealed class ReferenceTypeData : IEnumerable<object?[]>
{
    #region Data

    /// <summary>
    /// Gets a collection of data for different c# value types.
    /// </summary>
    private IEnumerable<object?[]> Data { get; } = new List<object?[]>(new[] {
            new object?[] { (string)"foo" },
            new object?[] { (object?)null },
            new object?[] { (Exception)new SerializableException("Foo") }
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
