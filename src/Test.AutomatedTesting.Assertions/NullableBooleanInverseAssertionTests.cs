namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableBooleanInverseAssertions"/> type.
/// </summary>
public sealed class NullableBooleanInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Nullable bool should not be (throws)")]
    public void NullableBoolShouldNotBeExpectedValueFailed()
    {
        // Given
        bool? b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().Be(false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("not to be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should not be")]
    public void NullableBoolShouldNotBeExpectedValue()
    {
        // Given
        bool? b = false;

        // When
        b.ShouldNot().Be(true);

        // Then
    }

    #endregion

    #region Should not be true

    [Fact(DisplayName = "Nullable bool should not be true (throws)")]
    public void NullableBoolShouldNotBeTrueFailed()
    {
        // Given
        bool? b = true;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().BeTrue());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"True\"", exception.Message);
        Assert.Contains("not to be \"True\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should not be true")]
    public void NullableBoolShouldNotBeTrue()
    {
        // Given
        bool? b = null;

        // When
        b.ShouldNot().BeTrue();

        // Then
    }

    #endregion

    #region Should not be false

    [Fact(DisplayName = "Nullable bool should not be false (throws)")]
    public void NullableBoolShouldNotBeFalseFailed()
    {
        // Given
        bool? b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().BeFalse());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("not to be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should not be false")]
    public void NullableBoolShouldNotBeFalse()
    {
        // Given
        bool? b = null;

        // When
        b.ShouldNot().BeFalse();

        // Then
    }

    #endregion

    #region Should not be null

    [Fact(DisplayName = "Nullable bool should not be null (throws)")]
    public void NullableBoolShouldNotBeNullFailed()
    {
        // Given
        bool? b = null;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable bool should not be null")]
    public void NullableBoolShouldNotBeNull()
    {
        // Given
        bool? b = true;

        // When
        b.ShouldNot().BeNull();

        // Then
    }

    #endregion
}
