namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Can be used in combination with a <see cref="ClassDataAttribute"/> for theories (see <see cref="TheoryAttribute"/>)
    /// that test different value types (supplies two parameters).
    /// </summary>
    public sealed class TwoParameterValueTypeData : IEnumerable<object[]>
    {
        #region Data

        /// <summary>
        /// Gets a collection of data for different c# value types.
        /// </summary>
        private List<object[]> Data { get; } = new List<object[]> {
                new object[] { (bool)true, (bool)false },
                new object[] { (byte)13, (byte)42 },
                new object[] { (short)13, (short)42 },
                new object[] { (ushort)13, (ushort)42 },
                new object[] { (int)13, (int)42 },
                new object[] { (uint)13, (uint)42 },
                new object[] { (long)13, (long)42 },
                new object[] { (ulong)13, (ulong)42 },
                new object[] { (float)13, (float)42 },
                new object[] { (double)13, (double)42 },
                // new object[] { (decimal)13, (decimal)42 }, currently an issue in xunit => https://github.com/xunit/xunit/issues/1771
                new object[] { StringComparison.OrdinalIgnoreCase, StringComparison.Ordinal }, // enum
                new object[] { (DateTime)DateTime.Today, DateTime.MaxValue } // struct
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
}
