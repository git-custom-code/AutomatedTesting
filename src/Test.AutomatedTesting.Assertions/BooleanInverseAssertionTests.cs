namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="BooleanInverseAssertions"/> type.
/// </summary>
public sealed class BooleanInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Bool should not be (throws)")]
    public void BoolShouldNotBeExpectedValueFailed()
    {
        // Given
        var b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().Be(false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("not to be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should not be")]
    public void BoolShouldNotBeExpectedValue()
    {
        // Given
        var b = false;

        // When
        b.ShouldNot().Be(true);

        // Then
    }

    #endregion

    #region Should not be true

    [Fact(DisplayName = "Bool should not be true (throws)")]
    public void BoolShouldNotBeTrueFailed()
    {
        // Given
        var b = true;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().BeTrue());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"True\"", exception.Message);
        Assert.Contains("not to be \"True\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should not be true")]
    public void BoolShouldNotBeTrue()
    {
        // Given
        var b = false;

        // When
        b.ShouldNot().BeTrue();

        // Then
    }

    #endregion

    #region Should not be false

    [Fact(DisplayName = "Bool should not be false (throws)")]
    public void BoolShouldNotBeFalseFailed()
    {
        // Given
        var b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.ShouldNot().BeFalse());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("not to be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should not be false")]
    public void BoolShouldNotBeFalse()
    {
        // Given
        var b = true;

        // When
        b.ShouldNot().BeFalse();

        // Then
    }

    #endregion
}
