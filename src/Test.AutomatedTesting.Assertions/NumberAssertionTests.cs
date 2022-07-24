namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NumberAssertions{T}"/> type.
/// </summary>
public sealed class NumberAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Number should be (throws)")]
    public void NumberShouldBeExpectedValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().Be(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be")]
    public void NumberShouldBeExpectedValue()
    {
        // Given
        var i = 13;

        // When
        i.Should().Be(13);

        // Then
    }

    #endregion

    #region Should be approximately

    [Fact(DisplayName = "Number should be approximately (throws)")]
    public void NumberShouldBeApproximatedValueFailed()
    {
        // Given
        var i = 13d;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeApproximately(42, tolerance: 0.1));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be approximately \"42\"", exception.Message);
        Assert.Contains("+-0.1", exception.Message);
    }

    [Fact(DisplayName = "Number should be approximately")]
    public void NumberShouldBeApproximatedValue()
    {
        // Given
        var i = 13.0001;

        // When
        i.Should().BeApproximately(13, tolerance: 0.1);

        // Then
    }

    #endregion

    #region Should be between

    [Fact(DisplayName = "Number should be between (throws)")]
    public void NumberShouldBeBetweenTwoValuesFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeBetween(42, 65));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be between \"42\" and \"65\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be between")]
    public void NumberShouldBeBetweenTwoValues()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeBetween(12, 14);

        // Then
    }

    #endregion

    #region Should be greater than

    [Fact(DisplayName = "Number should be greater than (throws)")]
    public void NumberShouldBeGreaterThanValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeGreaterThan(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be greater than \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be greater than")]
    public void NumberShouldBeGreaterThanValue()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeGreaterThan(9);

        // Then
    }

    #endregion

    #region Should be greater than or equal

    [Fact(DisplayName = "Number should be greater than or equal (throws)")]
    public void NumberShouldBeGreaterThanOrEqualToValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeGreaterThanOrEqualTo(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be greater than or equal to \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be greater than or equal")]
    public void NumberShouldBeGreaterThanOrEqualToValue()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeGreaterThanOrEqualTo(13);

        // Then
    }

    #endregion

    #region Should be less than

    [Fact(DisplayName = "Number should be less than (throws)")]
    public void NumberShouldBeLessThanValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeLessThan(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be less than \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be less than")]
    public void NumberShouldBeLessThanValue()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeLessThan(42);

        // Then
    }

    #endregion

    #region Should be greater than or equal

    [Fact(DisplayName = "Number should be less than or equal (throws)")]
    public void NumberShouldBeLessThanOrEqualToValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeLessThanOrEqualTo(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be less than or equal to \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be less than or equal")]
    public void NumberShouldBeLessThanOrEqualToValue()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeGreaterThanOrEqualTo(13);

        // Then
    }

    #endregion

    #region Should be negative

    [Fact(DisplayName = "Number should be negative (throws)")]
    public void NumberShouldBeNegativeFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeNegative());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be a negative number", exception.Message);
    }

    [Fact(DisplayName = "Number should be negative")]
    public void NumberShouldBeNegative()
    {
        // Given
        var i = -13;

        // When
        i.Should().BeNegative();

        // Then
    }

    #endregion

    #region Should be one of

    [Fact(DisplayName = "Number should be one of (throws)")]
    public void NumberShouldBeOneOfFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeOneOf(new[] { 9, 42 }));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be one of the following values: \"9\", \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should be one of")]
    public void NumberShouldBeOneOf()
    {
        // Given
        var i = 13;

        // When
        i.Should().BeOneOf(new[] { 9, 13, 42 });

        // Then
    }

    #endregion

    #region Should be positive

    [Fact(DisplayName = "Number should be positive (throws)")]
    public void NumberShouldBePositiveFailed()
    {
        // Given
        var i = -13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BePositive());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"-13\"", exception.Message);
        Assert.Contains("be a positive number", exception.Message);
    }

    [Fact(DisplayName = "Number should be positive")]
    public void NumberShouldBePositive()
    {
        // Given
        var i = 0;

        // When
        i.Should().BePositive();

        // Then
    }

    #endregion

    #region Should fulfill

    [Fact(DisplayName = "Number should fulfill (throws)")]
    public void NumberShouldFulfillConditionFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().Fulfill(x => x != 13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("to fulfill condition: \"x => (x != 13)\"", exception.Message);
    }

    [Fact(DisplayName = "Number should fulfill")]
    public void NumberShouldFulfillCondition()
    {
        // Given
        var i = 13;

        // When
        i.Should().Fulfill(x => x == 13);

        // Then
    }

    #endregion
}
