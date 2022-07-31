namespace CustomCode.AutomatedTesting.Assertions;

using System;

/// <summary>
/// Extension methods for enumerations to enable <see cref="Should{T}(T)"/> and <see cref="ShouldNot{T}(T)"/> assertions.
/// </summary>
public static class ShouldExtensionsEnum
{
    #region Logic

    /// <summary>
    /// Assertions for <see cref="Enum"/> data types.
    /// </summary>
    /// <param name="enumeration"> The enumeration to be checked. </param>
    /// <returns> A <see cref="EnumAssertions{T}"/> instance for specifying assertions. </returns>
    public static EnumAssertions<T> Should<T>(this T enumeration)
        where T : Enum
    {
        return new EnumAssertions<T>(enumeration);
    }

    /// <summary>
    /// Assertions for nullable <see cref="Enum"/> data types.
    /// </summary>
    /// <param name="nullableEnumeration"> The nullable enumeration to be checked. </param>
    /// <returns> A <see cref="NullableEnumAssertions{T}"/> instance for specifying assertions. </returns>
    public static NullableEnumAssertions<T> Should<T>(this Nullable<T> nullableEnumeration)
        where T : struct, Enum
    {
        return new NullableEnumAssertions<T>(nullableEnumeration);
    }

    /// <summary>
    /// Inverse assertions for <see cref="Enum"/> data types.
    /// </summary>
    /// <param name="enumeration"> The enumeration to be checked. </param>
    /// <returns> A <see cref="EnumInverseAssertions{T}"/> instance for specifying assertions. </returns>
    public static EnumInverseAssertions<T> ShouldNot<T>(this T enumeration)
        where T : Enum
    {
        return new EnumInverseAssertions<T>(enumeration);
    }

    /// <summary>
    /// Inverse assertions for nullable <see cref="Enum"/> data types.
    /// </summary>
    /// <param name="nullableEnumeration"> The nullable enumeration to be checked. </param>
    /// <returns> A <see cref="NullableEnumInverseAssertions{T}"/> instance for specifying assertions. </returns>
    public static NullableEnumInverseAssertions<T> ShouldNot<T>(this Nullable<T> nullableEnumeration)
        where T : struct, Enum
    {
        return new NullableEnumInverseAssertions<T>(nullableEnumeration);
    }

    #endregion
}
