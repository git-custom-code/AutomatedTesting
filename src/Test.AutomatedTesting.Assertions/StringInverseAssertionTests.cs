namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="StringInverseAssertions"/> type.
/// </summary>
public sealed class StringInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "String should not be (throws)")]
    public void StringShouldNotBeExpectedValueFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().Be("foo"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\"", exception.Message);
        Assert.Contains("not to be \"foo\"", exception.Message);
    }

    [Fact(DisplayName = "String should not be")]
    public void StringShouldNotBeExpectedValue()
    {
        // Given
        var s = "foo";

        // When
        s.ShouldNot().Be("bar");

        // Then
    }

    #endregion

    #region Should not be empty

    [Fact(DisplayName = "String should not be empty (throws)")]
    public void StringShouldNotBeEmptyFailed()
    {
        // Given
        var s = string.Empty;

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().BeEmpty());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be empty", exception.Message);
    }

    [Fact(DisplayName = "String should not be empty")]
    public void StringShouldNotBeEmpty()
    {
        // Given
        var s = "foo";

        // When
        s.ShouldNot().BeEmpty();

        // Then
    }

    #endregion

    #region Should not be null

    [Fact(DisplayName = "String should not be null (throws)")]
    public void StringShouldNotBeNullFailed()
    {
        // Given
        string? s = null;

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be null", exception.Message);
    }

    [Fact(DisplayName = "String should not be null")]
    public void StringShouldNotBeNull()
    {
        // Given
        var s = "foo";

        // When
        s.ShouldNot().BeNull();

        // Then
    }

    #endregion

    #region Should not be null or empty

    [Fact(DisplayName = "String should not be null or empty (throws)")]
    public void StringShouldNotBeNullOrEmptyFailed()
    {
        // Given
        var s = string.Empty;

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().BeNullOrEmpty());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be null or empty", exception.Message);
    }

    [Fact(DisplayName = "String should not be null or empty")]
    public void StringShouldNotBeNullOrEmpty()
    {
        // Given
        var s = "foo";

        // When
        s.ShouldNot().BeNullOrEmpty();

        // Then
    }

    #endregion

    #region Should not contain

    [Fact(DisplayName = "String should not contain (throws)")]
    public void StringShouldNotContainFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().Contain("BAR", ignoreCase: false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("not to contain \"BAR\"", exception.Message);
    }

    [Fact(DisplayName = "String should not contain")]
    public void StringShouldNotContain()
    {
        // Given
        var s = "fooBAR";

        // When
        s.ShouldNot().Contain("bar", ignoreCase: false);

        // Then
    }

    #endregion

    #region Should not end with

    [Fact(DisplayName = "String should not end with (throws)")]
    public void StringShouldNotEndWithFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().EndWith("bar", ignoreCase: true));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("not to end with \"bar\"", exception.Message);
    }

    [Fact(DisplayName = "String should not end with")]
    public void StringShouldNotEndWith()
    {
        // Given
        var s = "fooBAR";

        // When
        s.ShouldNot().EndWith("foo");

        // Then
    }

    #endregion

    #region Should not have length

    [Fact(DisplayName = "String should not have length (throws)")]
    public void StringShouldNotHaveLengthFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().HaveLength(3));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\" (with a length of \"3\")", exception.Message);
        Assert.Contains("not to have a length of \"3\"", exception.Message);
    }

    [Fact(DisplayName = "String should not have length")]
    public void StringShouldNotHaveLength()
    {
        // Given
        var s = "foo";

        // When
        s.ShouldNot().HaveLength(13);

        // Then
    }

    #endregion

    #region Should not match

    [Fact(DisplayName = "String should not match (throws)")]
    public void StringShouldNotMatchFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().Match("foo*"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("not to match pattern \"foo*\"", exception.Message);
    }

    [Fact(DisplayName = "String should not match")]
    public void StringShouldNotMatch()
    {
        // Given
        var s = "fooBAR";

        // When
        s.ShouldNot().Match("foo");

        // Then
    }

    #endregion

    #region Should not match regex

    [Fact(DisplayName = "String should not match regex (throws)")]
    public void StringShouldNotMatchRegexFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().MatchRegex("foo"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("not to match regular expression \"foo\"", exception.Message);
    }

    [Fact(DisplayName = "String should not match regex")]
    public void StringShouldNotMatchRegex()
    {
        // Given
        var s = "fooBAR";

        // When
        s.ShouldNot().MatchRegex("^foo$");

        // Then
    }

    #endregion

    #region Should not start with

    [Fact(DisplayName = "String should not start with (throws)")]
    public void StringShouldNotStartWithFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.ShouldNot().StartWith("foo"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("not to start with \"foo\"", exception.Message);
    }

    [Fact(DisplayName = "String should not start with")]
    public void StringShouldNotStartWith()
    {
        // Given
        var s = "fooBAR";

        // When
        s.ShouldNot().StartWith("bar");

        // Then
    }

    #endregion
}
