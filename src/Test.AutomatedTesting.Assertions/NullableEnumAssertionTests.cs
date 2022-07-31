namespace CustomCode.AutomatedTesting.Assertions.Tests;

using System.Reflection;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableEnumAssertions{T}"/> type.
/// </summary>
public sealed class NullableEnumAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Nullable enum should be (throws)")]
    public void NullableEnumShouldBeExpectedValueFailed()
    {
        // Given
        BindingFlags? enumeration = null;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.Should().Be(BindingFlags.NonPublic));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("be \"NonPublic\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should be")]
    public void NullableEnumShouldBeExpectedValue()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public;

        // When
        enumeration.Should().Be(BindingFlags.Public);

        // Then
    }

    #endregion

    #region Should have flag

    [Fact(DisplayName = "Nullable enum should have flag (throws)")]
    public void NullableEnumShouldHaveFlagFailed()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.Should().HaveFlag(BindingFlags.NonPublic));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Instance, Public\"", exception.Message);
        Assert.Contains("to have flag \"NonPublic\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should have flag")]
    public void NullableEnumShouldHaveFlag()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        enumeration.Should().HaveFlag(BindingFlags.Instance);

        // Then
    }

    #endregion

    #region Should be null

    [Fact(DisplayName = "Nullable enum should be null (throws)")]
    public void NullableEnumShouldBeNullFailed()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.Should().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Public\"", exception.Message);
        Assert.Contains("to be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should be null")]
    public void NullableEnumShouldBeNull()
    {
        // Given
        BindingFlags? enumeration = null;

        // When
        enumeration.Should().BeNull();

        // Then
    }

    #endregion
}
