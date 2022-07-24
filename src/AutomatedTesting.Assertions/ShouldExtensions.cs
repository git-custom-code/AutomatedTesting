namespace CustomCode.AutomatedTesting.Assertions;

using System;

/// <summary>
/// Extension methods for various types to enable <see cref="Should{T}(T)"/> and <see cref="ShouldNot{T}(T)"/> assertions.
/// </summary>
public static class ShouldExtensions
{
    #region Logic

    /// <summary>
    /// Assertions for numeric data types.
    /// </summary>
    /// <typeparam name="T"> The <paramref name="number"/>'s data type (i.e. <see cref="int"/> or <see cref="double"/>). </typeparam>
    /// <param name="number"> The number to be checked. </param>
    /// <returns> A <see cref="NumberAssertions{T}"/> instance for specifying assertions. </returns>
    public static NumberAssertions<T> Should<T>(this T number)
        where T : INumber<T>
    {
        return new NumberAssertions<T>(number);
    }

    /// <summary>
    /// Assertions for nullable numeric data types.
    /// </summary>
    /// <typeparam name="T"> The <paramref name="nullableNumber"/>'s data type (i.e. <see cref="int"/> or <see cref="double"/>). </typeparam>
    /// <param name="nullableNumber"> The nullable number to be checked. </param>
    /// <returns> A <see cref="NullableNumberAssertions{T}"/> instance for specifying assertions. </returns>
    public static NullableNumberAssertions<T> Should<T>(this Nullable<T> nullableNumber)
        where T : struct, INumber<T>
    {
        return new NullableNumberAssertions<T>(nullableNumber);
    }

    /// <summary>
    /// Inverse assertions for numeric data types.
    /// </summary>
    /// <typeparam name="T"> The <paramref name="number"/>'s data type (i.e. <see cref="int"/> or <see cref="double"/>). </typeparam>
    /// <param name="number"> The number to be checked. </param>
    /// <returns> A <see cref="NumberInverseAssertions{T}"/> instance for specifying assertions. </returns>
    public static NumberInverseAssertions<T> ShouldNot<T>(this T number)
        where T : INumber<T>
    {
        return new NumberInverseAssertions<T>(number);
    }

    /// <summary>
    /// Inverse assertions for nullable numeric data types.
    /// </summary>
    /// <typeparam name="T"> The <paramref name="nullableNumber"/>'s data type (i.e. <see cref="int"/> or <see cref="double"/>). </typeparam>
    /// <param name="nullableNumber"> The nullable number to be checked. </param>
    /// <returns> A <see cref="NumberInverseAssertions{T}"/> instance for specifying assertions. </returns>
    public static NullableNumberInverseAssertions<T> ShouldNot<T>(this Nullable<T> nullableNumber)
        where T : struct, INumber<T>
    {
        return new NullableNumberInverseAssertions<T>(nullableNumber);
    }

    #endregion
}
