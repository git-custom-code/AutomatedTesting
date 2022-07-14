namespace CustomCode.AutomatedTesting.Mocks.Core.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

/// <summary>
/// Can be used in combination with a <see cref="ClassDataAttribute"/> for theories (see <see cref="TheoryAttribute"/>)
/// that test different value types (supplies one parameter).
/// </summary>
public sealed class ValueTypeData : IEnumerable<object[]>
{
    #region Data

    /// <summary>
    /// Gets a collection of data for different c# value types.
    /// </summary>
    private List<object[]> Data { get; } = new List<object[]> {
            new object[] { (bool)true },
            new object[] { (byte)13 },
            new object[] { (short)13 },
            new object[] { (ushort)13 },
            new object[] { (int)13 },
            new object[] { (uint)13 },
            new object[] { (long)13 },
            new object[] { (ulong)13 },
            new object[] { (float)13 },
            new object[] { (double)13 },
            // new object[] { (decimal)13 }, currently an issue in xunit => https://github.com/xunit/xunit/issues/1771
            new object[] { StringComparison.OrdinalIgnoreCase }, // enum
            new object[] { (DateTime)DateTime.Today } // struct
        };

    #endregion

    #region Logic

    /// <inheritdoc />
    public IEnumerator<object[]> GetEnumerator()
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
