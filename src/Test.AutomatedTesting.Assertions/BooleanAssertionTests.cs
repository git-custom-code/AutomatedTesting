namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="BooleanAssertions"/> type.
/// </summary>
public sealed class BooleanAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Bool should be (throws)")]
    public void BoolShouldBeExpectedValueFailed()
    {
        // Given
        var b = true;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().Be(false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"True\"", exception.Message);
        Assert.Contains("be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should be")]
    public void BoolShouldBeExpectedValue()
    {
        // Given
        var b = true;

        // When
        b.Should().Be(true);

        // Then
    }

    #endregion

    #region Should be true

    [Fact(DisplayName = "Bool should be true (throws)")]
    public void BoolShouldBeTrueFailed()
    {
        // Given
        var b = false;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().BeTrue());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"False\"", exception.Message);
        Assert.Contains("be \"True\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should be true")]
    public void BoolShouldBeTrue()
    {
        // Given
        var b = true;

        // When
        b.Should().BeTrue();

        // Then
    }

    #endregion

    #region Should be false

    [Fact(DisplayName = "Bool should be false (throws)")]
    public void BoolShouldBeFalseFailed()
    {
        // Given
        var b = true;

        // When
        var exception = Assert.Throws<XunitException>(() => b.Should().BeFalse());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"True\"", exception.Message);
        Assert.Contains("be \"False\"", exception.Message);
    }

    [Fact(DisplayName = "Bool should be false")]
    public void BoolShouldBeFalse()
    {
        // Given
        var b = false;

        // When
        b.Should().BeFalse();

        // Then
    }

    #endregion
}
