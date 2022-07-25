namespace CustomCode.AutomatedTesting.Assertions;

using System;

/// <summary>
/// Extension methods for enumerations to enable <see cref="Should{T}(T)"/> and <see cref="ShouldNot{T}(T)"/> assertions.
/// </summary>
public static class ShouldExtensionsEnum
{
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
    /// Inverse assertions for <see cref="Enum"/> data types.
    /// </summary>
    /// <param name="enumeration"> The enumeration to be checked. </param>
    /// <returns> A <see cref="EnumInverseAssertions{T}"/> instance for specifying assertions. </returns>
    public static EnumInverseAssertions<T> ShouldNot<T>(this T enumeration)
        where T : Enum
    {
        return new EnumInverseAssertions<T>(enumeration);
    }
}
