namespace CustomCode.AutomatedTesting.Assertions.Tests;

using System.Reflection;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="EnumAssertions{T}"/> type.
/// </summary>
public sealed class EnumAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Enum should be (throws)")]
    public void EnumShouldBeExpectedValueFailed()
    {
        // Given
        var enumeration = BindingFlags.Public;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.Should().Be(BindingFlags.NonPublic));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Public\"", exception.Message);
        Assert.Contains("be \"NonPublic\"", exception.Message);
    }

    [Fact(DisplayName = "Enum should be")]
    public void EnumShouldBeExpectedValue()
    {
        // Given
        var enumeration = BindingFlags.Public;

        // When
        enumeration.Should().Be(BindingFlags.Public);

        // Then
    }

    #endregion

    #region Should have flag

    [Fact(DisplayName = "Enum should have flag (throws)")]
    public void EnumShouldHaveFlagFailed()
    {
        // Given
        var enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.Should().HaveFlag(BindingFlags.NonPublic));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Instance, Public\"", exception.Message);
        Assert.Contains("to have flag \"NonPublic\"", exception.Message);
    }

    [Fact(DisplayName = "Enum should have flag")]
    public void EnumShouldHaveFlag()
    {
        // Given
        var enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        enumeration.Should().HaveFlag(BindingFlags.Instance);

        // Then
    }

    #endregion
}
