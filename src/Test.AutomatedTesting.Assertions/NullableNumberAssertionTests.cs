namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableNumberAssertions{T}"/> type.
/// </summary>
public sealed class NullableNumberAssertionTests
{
    #region Should be

    [Fact(DisplayName = "Nullable Number should be (throws)")]
    public void NullableNumberShouldBeExpectedValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().Be(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be")]
    public void NullableNumberShouldBeExpectedValue()
    {
        // Given
        int? i = 13;

        // When
        i.Should().Be(13);

        // Then
    }

    #endregion

    #region Should be approximately

    [Fact(DisplayName = "Nullable Number should be approximately (throws)")]
    public void NullableNumberShouldBeApproximatedValueFailed()
    {
        // Given
        double? i = 13d;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeApproximately(42, tolerance: 0.1));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be approximately \"42\"", exception.Message);
        Assert.Contains("+-0.1", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be approximately")]
    public void NullableNumberShouldBeApproximatedValue()
    {
        // Given
        double? i = 13.0001;

        // When
        i.Should().BeApproximately(13, tolerance: 0.1);

        // Then
    }

    #endregion

    #region Should be between

    [Fact(DisplayName = "Nullable Number should be between (throws)")]
    public void NullableNumberShouldBeBetweenTwoValuesFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeBetween(42, 65));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be between \"42\" and \"65\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be between")]
    public void NullableNumberShouldBeBetweenTwoValues()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeBetween(12, 14);

        // Then
    }

    #endregion

    #region Should be greater than

    [Fact(DisplayName = "Nullable Number should be greater than (throws)")]
    public void NullableNumberShouldBeGreaterThanValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeGreaterThan(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be greater than \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be greater than")]
    public void NullableNumberShouldBeGreaterThanValue()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeGreaterThan(9);

        // Then
    }

    #endregion

    #region Should be greater than or equal

    [Fact(DisplayName = "Nullable Number should be greater than or equal (throws)")]
    public void NullableNumberShouldBeGreaterThanOrEqualToValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeGreaterThanOrEqualTo(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be greater than or equal to \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be greater than or equal")]
    public void NullableNumberShouldBeGreaterThanOrEqualToValue()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeGreaterThanOrEqualTo(13);

        // Then
    }

    #endregion

    #region Should be less than

    [Fact(DisplayName = "Nullable Number should be less than (throws)")]
    public void NullableNumberShouldBeLessThanValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeLessThan(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be less than \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be less than")]
    public void NullableNumberShouldBeLessThanValue()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeLessThan(42);

        // Then
    }

    #endregion

    #region Should be greater than or equal

    [Fact(DisplayName = "Nullable Number should be less than or equal (throws)")]
    public void NullableNumberShouldBeLessThanOrEqualToValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeLessThanOrEqualTo(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be less than or equal to \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be less than or equal")]
    public void NullableNumberShouldBeLessThanOrEqualToValue()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeGreaterThanOrEqualTo(13);

        // Then
    }

    #endregion

    #region Should be negative

    [Fact(DisplayName = "Nullable Number should be negative (throws)")]
    public void NullableNumberShouldBeNegativeFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeNegative());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be a negative number", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be negative")]
    public void NullableNumberShouldBeNegative()
    {
        // Given
        int? i = -13;

        // When
        i.Should().BeNegative();

        // Then
    }

    #endregion

    #region Should be null

    [Fact(DisplayName = "Nullable Number should be null (throws)")]
    public void NullableNumberShouldBeNullFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be null")]
    public void NullableNumberShouldBeNull()
    {
        // Given
        int? i = null;

        // When
        i.Should().BeNull();

        // Then
    }

    #endregion

    #region Should be one of

    [Fact(DisplayName = "Nullable Number should be one of (throws)")]
    public void NullableNumberShouldBeOneOfFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BeOneOf(new[] { 9, 42 }));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("be one of the following values: \"9\", \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be one of")]
    public void NullableNumberShouldBeOneOf()
    {
        // Given
        int? i = 13;

        // When
        i.Should().BeOneOf(new[] { 9, 13, 42 });

        // Then
    }

    #endregion

    #region Should be positive

    [Fact(DisplayName = "Nullable Number should be positive (throws)")]
    public void NullableNumberShouldBePositiveFailed()
    {
        // Given
        int? i = -13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().BePositive());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"-13\"", exception.Message);
        Assert.Contains("be a positive number", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should be positive")]
    public void NullableNumberShouldBePositive()
    {
        // Given
        int? i = 0;

        // When
        i.Should().BePositive();

        // Then
    }

    #endregion

    #region Should fulfill

    [Fact(DisplayName = "Nullable Number should fulfill (throws)")]
    public void NullableNumberShouldFulfillConditionFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.Should().Fulfill(x => x != 13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("to fulfill condition: \"x => (x !=", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should fulfill")]
    public void NullableNumberShouldFulfillCondition()
    {
        // Given
        int? i = 13;

        // When
        i.Should().Fulfill(x => x == 13);

        // Then
    }

    #endregion
}
