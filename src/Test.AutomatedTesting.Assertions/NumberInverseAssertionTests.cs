namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NumberInverseAssertions{T}"/> type.
/// </summary>
public sealed class NumberInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Number should not be (throws)")]
    public void NumberShouldNotBeExpectedValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().Be(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be")]
    public void NumberShouldNotBeExpectedValue()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().Be(42);

        // Then
    }

    #endregion

    #region Should not be approximately

    [Fact(DisplayName = "Number should not be approximately (throws)")]
    public void NumberShouldNotBeApproximatedValueFailed()
    {
        // Given
        var i = 13.005;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeApproximately(13d, tolerance: 0.1));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13.005\"", exception.Message);
        Assert.Contains("not to be approximately \"13\"", exception.Message);
        Assert.Contains("+-0.1", exception.Message);
    }

    [Fact(DisplayName = "Number should not be approximately")]
    public void NumberShouldNotBeApproximatedValue()
    {
        // Given
        var i = 13d;

        // When
        i.ShouldNot().BeApproximately(42d, tolerance: 0.1);

        // Then
    }

    #endregion

    #region Should not be between

    [Fact(DisplayName = "Number should not be between (throws)")]
    public void NumberShouldNotBeBetweenTwoValuesFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeBetween(9, 42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be between \"9\" and \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be between")]
    public void NumberShouldNotBeBetweenTwoValues()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeBetween(42, 65);

        // Then
    }

    #endregion

    #region Should not be greater than

    [Fact(DisplayName = "Number should not be greater than (throws)")]
    public void NumberShouldNotBeGreaterThanValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeGreaterThan(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be greater than \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be greater than")]
    public void NumberShouldNotBeGreaterThanValue()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeGreaterThan(42);

        // Then
    }

    #endregion

    #region Should not be greater than or equal

    [Fact(DisplayName = "Number should not be greater than or equal (throws)")]
    public void NumberShouldNotBeGreaterThanOrEqualToValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeGreaterThanOrEqualTo(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be greater than or equal to \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be greater than or equal")]
    public void NumberShouldNotBeGreaterThanOrEqualToValue()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeGreaterThanOrEqualTo(42);

        // Then
    }

    #endregion

    #region Should not be less than

    [Fact(DisplayName = "Number should not be less than (throws)")]
    public void NumberShouldNotBeLessThanValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeLessThan(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be less than \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be less than")]
    public void NumberShouldNotBeLessThanValue()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeLessThan(9);

        // Then
    }

    #endregion

    #region Should not be greater than or equal

    [Fact(DisplayName = "Number should not be less than or equal (throws)")]
    public void NumberShouldNotBeLessThanOrEqualToValueFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeLessThanOrEqualTo(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be less than or equal to \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be less than or equal")]
    public void NumberShouldNotBeLessThanOrEqualToValue()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeGreaterThanOrEqualTo(42);

        // Then
    }

    #endregion

    #region Should not be negative

    [Fact(DisplayName = "Number should not be negative (throws)")]
    public void NumberShouldNotBeNegativeFailed()
    {
        // Given
        var i = -13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeNegative());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"-13\"", exception.Message);
        Assert.Contains("not to be a negative number", exception.Message);
    }

    [Fact(DisplayName = "Number should not be negative")]
    public void NumberShouldNotBeNegative()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeNegative();

        // Then
    }

    #endregion

    #region Should not be one of

    [Fact(DisplayName = "Number should not be one of (throws)")]
    public void NumberShouldNotBeOneOfFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeOneOf(new[] { 9, 13 }));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be one of the following values: \"9\", \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not be one of")]
    public void NumberShouldNotBeOneOf()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().BeOneOf(new[] { 9, 42 });

        // Then
    }

    #endregion

    #region Should not be positive

    [Fact(DisplayName = "Number should not be positive (throws)")]
    public void NumberShouldNotBePositiveFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BePositive());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be a positive number", exception.Message);
    }

    [Fact(DisplayName = "Number should not be positive")]
    public void NumberShouldNotBePositive()
    {
        // Given
        var i = -1;

        // When
        i.ShouldNot().BePositive();

        // Then
    }

    #endregion

    #region Should not fulfill

    [Fact(DisplayName = "Number should not fulfill (throws)")]
    public void NumberShouldNotFulfillConditionFailed()
    {
        // Given
        var i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().Fulfill(x => x == 13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to fulfill condition: \"x => (x == 13)\"", exception.Message);
    }

    [Fact(DisplayName = "Number should not fulfill")]
    public void NumberShouldNotFulfillCondition()
    {
        // Given
        var i = 13;

        // When
        i.ShouldNot().Fulfill(x => x != 13);

        // Then
    }

    #endregion
}
