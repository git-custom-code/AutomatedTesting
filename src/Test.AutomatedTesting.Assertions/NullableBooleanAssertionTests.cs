namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableBooleanAssertions"/> type.
/// </summary>
public sealed class NullableBooleanAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Nullable bool should be (throws)")]
    public void NullableBoolShouldBeExpectedValueFailed()
    {
        // Given
        bool? b = null;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().Be(false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should be")]
    public void NullableBoolShouldBeExpectedValue()
    {
        // Given
        bool? b = true;

        // When
        b.Should().Be(true);

        // Then
    }

    #endregion

    #region Should be true

    [Fact(DisplayName = "Nullable bool should be true (throws)")]
    public void NullableBoolShouldBeTrueFailed()
    {
        // Given
        bool? b = null;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().BeTrue());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("be \"True\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should be true")]
    public void NullableBoolShouldBeTrue()
    {
        // Given
        bool? b = true;

        // When
        b.Should().BeTrue();

        // Then
    }

    #endregion

    #region Should be false

    [Fact(DisplayName = "Nullable bool should be false (throws)")]
    public void NullableBoolShouldBeFalseFailed()
    {
        // Given
        bool? b = null;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().BeFalse());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should be false")]
    public void NullableBoolShouldBeFalse()
    {
        // Given
        bool? b = false;

        // When
        b.Should().BeFalse();

        // Then
    }

    #endregion

    #region Should be null

    [Fact(DisplayName = "Nullable bool should be null (throws)")]
    public void NullableBoolShouldBeNullFailed()
    {
        // Given
        bool? b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should be null")]
    public void NullableBoolShouldBeNull()
    {
        // Given
        bool? b = null;

        // When
        b.Should().BeNull();

        // Then
    }

    #endregion
}
