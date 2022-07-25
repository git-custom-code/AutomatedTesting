namespace CustomCode.AutomatedTesting.Assertions.Tests;

using System.Reflection;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="EnumInverseAssertions{T}"/> type.
/// </summary>
public sealed class EnumInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Enum should not be (throws)")]
    public void EnumShouldNotBeExpectedValueFailed()
    {
        // Given
        var enumeration = BindingFlags.Public;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.ShouldNot().Be(BindingFlags.Public));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Public\"", exception.Message);
        Assert.Contains("not to be \"Public\"", exception.Message);
    }

    [Fact(DisplayName = "Enum should not be")]
    public void EnumShouldNotBeExpectedValue()
    {
        // Given
        var enumeration = BindingFlags.Public;

        // When
        enumeration.ShouldNot().Be(BindingFlags.NonPublic);

        // Then
    }

    #endregion

    #region Should not have flag

    [Fact(DisplayName = "Enum should not have flag (throws)")]
    public void EnumShouldNotHaveFlagFailed()
    {
        // Given
        var enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.ShouldNot().HaveFlag(BindingFlags.Public));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Instance, Public\"", exception.Message);
        Assert.Contains("not to have flag \"Public\"", exception.Message);
    }

    [Fact(DisplayName = "Enum should not have flag")]
    public void EnumShouldNotHaveFlag()
    {
        // Given
        var enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        enumeration.ShouldNot().HaveFlag(BindingFlags.NonPublic);

        // Then
    }

    #endregion
}
