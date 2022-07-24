namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableNumberInverseAssertions{T}"/> type.
/// </summary>
public sealed class NullableNumberInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Nullable Number should not be (throws)")]
    public void NullableNumberShouldNotBeExpectedValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().Be(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be")]
    public void NullableNumberShouldNotBeExpectedValue()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().Be(42);

        // Then
    }

    #endregion

    #region Should not be approximately

    [Fact(DisplayName = "Nullable Number should not be approximately (throws)")]
    public void NullableNumberShouldNotBeApproximatedValueFailed()
    {
        // Given
        double? i = 13.005;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeApproximately(13d, tolerance: 0.1));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13.005\"", exception.Message);
        Assert.Contains("not to be approximately \"13\"", exception.Message);
        Assert.Contains("+-0.1", exception.Message);
    }

    [Fact(DisplayName = "NullableNumber should not be approximately")]
    public void NullableNumberShouldNotBeApproximatedValue()
    {
        // Given
        double? i = 13d;

        // When
        i.ShouldNot().BeApproximately(42d, tolerance: 0.1);

        // Then
    }

    #endregion

    #region Should not be between

    [Fact(DisplayName = "NullableNumber should not be between (throws)")]
    public void NullableNumberShouldNotBeBetweenTwoValuesFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeBetween(9, 42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be between \"9\" and \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be between")]
    public void NullableNumberShouldNotBeBetweenTwoValues()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeBetween(42, 65);

        // Then
    }

    #endregion

    #region Should not be greater than

    [Fact(DisplayName = "Nullable Number should not be greater than (throws)")]
    public void NullableNumberShouldNotBeGreaterThanValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeGreaterThan(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be greater than \"9\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be greater than")]
    public void NullableNumberShouldNotBeGreaterThanValue()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeGreaterThan(42);

        // Then
    }

    #endregion

    #region Should not be greater than or equal

    [Fact(DisplayName = "Nullable Number should not be greater than or equal (throws)")]
    public void NullableNumberShouldNotBeGreaterThanOrEqualToValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeGreaterThanOrEqualTo(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be greater than or equal to \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be greater than or equal")]
    public void NullableNumberShouldNotBeGreaterThanOrEqualToValue()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeGreaterThanOrEqualTo(42);

        // Then
    }

    #endregion

    #region Should not be less than

    [Fact(DisplayName = "Nullable Number should not be less than (throws)")]
    public void NullableNumberShouldNotBeLessThanValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeLessThan(42));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be less than \"42\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be less than")]
    public void NullableNumberShouldNotBeLessThanValue()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeLessThan(9);

        // Then
    }

    #endregion

    #region Should not be greater than or equal

    [Fact(DisplayName = "Nullable Number should not be less than or equal (throws)")]
    public void NullableNumberShouldNotBeLessThanOrEqualToValueFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeLessThanOrEqualTo(13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be less than or equal to \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be less than or equal")]
    public void NullableNumberShouldNotBeLessThanOrEqualToValue()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeGreaterThanOrEqualTo(42);

        // Then
    }

    #endregion

    #region Should not be negative

    [Fact(DisplayName = "Nullable Number should not be negative (throws)")]
    public void NullableNumberShouldNotBeNegativeFailed()
    {
        // Given
        int? i = -13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeNegative());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"-13\"", exception.Message);
        Assert.Contains("not to be a negative number", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be negative")]
    public void NullableNumberShouldNotBeNegative()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeNegative();

        // Then
    }

    #endregion

    #region Should be null

    [Fact(DisplayName = "Nullable Number should not be null (throws)")]
    public void NullableNumberShouldNotBeNullFailed()
    {
        // Given
        int? i = null;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be null")]
    public void NullableNumberShouldNotBeNull()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeNull();

        // Then
    }

    #endregion

    #region Should not be one of

    [Fact(DisplayName = "Nullable Number should not be one of (throws)")]
    public void NullableNumberShouldNotBeOneOfFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BeOneOf(new[] { 9, 13 }));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be one of the following values: \"9\", \"13\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be one of")]
    public void NullableNumberShouldNotBeOneOf()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().BeOneOf(new[] { 9, 42 });

        // Then
    }

    #endregion

    #region Should not be positive

    [Fact(DisplayName = "Nullable Number should not be positive (throws)")]
    public void NullableNumberShouldNotBePositiveFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().BePositive());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to be a positive number", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not be positive")]
    public void NullableNumberShouldNotBePositive()
    {
        // Given
        int? i = -1;

        // When
        i.ShouldNot().BePositive();

        // Then
    }

    #endregion

    #region Should not fulfill

    [Fact(DisplayName = "Nullable Number should not fulfill (throws)")]
    public void NullableNumberShouldNotFulfillConditionFailed()
    {
        // Given
        int? i = 13;

        // When
        var exception = Assert.Throws<XunitException>(() => i.ShouldNot().Fulfill(x => x == 13));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("i", exception.Message);
        Assert.Contains("is \"13\"", exception.Message);
        Assert.Contains("not to fulfill condition: \"x => (x ==", exception.Message);
    }

    [Fact(DisplayName = "Nullable Number should not fulfill")]
    public void NullableNumberShouldNotFulfillCondition()
    {
        // Given
        int? i = 13;

        // When
        i.ShouldNot().Fulfill(x => x != 13);

        // Then
    }

    #endregion
}
