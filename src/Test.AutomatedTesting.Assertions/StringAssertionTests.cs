namespace CustomCode.AutomatedTesting.Assertions.Tests;

using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="StringAssertions"/> type.
/// </summary>
public sealed class StringAssertionTests
{
    #region Should be

    [Fact(DisplayName = "String should be (throws)")]
    public void StringShouldBeExpectedValueFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().Be("bar"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\"", exception.Message);
        Assert.Contains("be \"bar\"", exception.Message);
    }

    [Fact(DisplayName = "String should be")]
    public void StringShouldBeExpectedValue()
    {
        // Given
        var s = "foo";

        // When
        s.Should().Be("foo");

        // Then
    }

    #endregion

    #region Should be empty

    [Fact(DisplayName = "String should be empty (throws)")]
    public void StringShouldBeEmptyFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().BeEmpty());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\"", exception.Message);
        Assert.Contains("be empty", exception.Message);
    }

    [Fact(DisplayName = "String should be empty")]
    public void StringShouldBeEmpty()
    {
        // Given
        var s = string.Empty;

        // When
        s.Should().BeEmpty();

        // Then
    }

    #endregion

    #region Should be null

    [Fact(DisplayName = "String should be null (throws)")]
    public void StringShouldBeNullFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\"", exception.Message);
        Assert.Contains("be null", exception.Message);
    }

    [Fact(DisplayName = "String should be null")]
    public void StringShouldBeNull()
    {
        // Given
        string? s = null;

        // When
        s.Should().BeNull();

        // Then
    }

    #endregion

    #region Should be null or empty

    [Fact(DisplayName = "String should be null or empty (throws)")]
    public void StringShouldBeNullOrEmptyFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().BeNullOrEmpty());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\"", exception.Message);
        Assert.Contains("be null or empty", exception.Message);
    }

    [Fact(DisplayName = "String should be null or empty")]
    public void StringShouldBeNullOrEmpty()
    {
        // Given
        string? s = null;

        // When
        s.Should().BeNullOrEmpty();

        // Then
    }

    #endregion

    #region Should contain

    [Fact(DisplayName = "String should contain (throws)")]
    public void StringShouldContainFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().Contain("bar", ignoreCase: false));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("contain \"bar\"", exception.Message);
    }

    [Fact(DisplayName = "String should contain")]
    public void StringShouldContain()
    {
        // Given
        var s = "fooBAR";

        // When
        s.Should().Contain("bar", ignoreCase: true);

        // Then
    }

    #endregion

    #region Should end with

    [Fact(DisplayName = "String should end with (throws)")]
    public void StringShouldEndWithFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().EndWith("foo"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("end with \"foo\"", exception.Message);
    }

    [Fact(DisplayName = "String should end with")]
    public void StringShouldEndWith()
    {
        // Given
        var s = "fooBAR";

        // When
        s.Should().EndWith("bar", ignoreCase: true);

        // Then
    }

    #endregion

    #region Should have length

    [Fact(DisplayName = "String should have length (throws)")]
    public void StringShouldHaveLengthFailed()
    {
        // Given
        var s = "foo";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().HaveLength(9));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"foo\" (with a length of \"3\")", exception.Message);
        Assert.Contains("to have a length of \"9\"", exception.Message);
    }

    [Fact(DisplayName = "String should have length")]
    public void StringShouldHaveLength()
    {
        // Given
        var s = "foo";

        // When
        s.Should().HaveLength(3);

        // Then
    }

    #endregion

    #region Should match

    [Fact(DisplayName = "String should match (throws)")]
    public void StringShouldMatchFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().Match("foo"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("match pattern \"foo\"", exception.Message);
    }

    [Fact(DisplayName = "String should match")]
    public void StringShouldMatch()
    {
        // Given
        var s = "fooBAR";

        // When
        s.Should().Match("foo*");

        // Then
    }

    #endregion

    #region Should match regex

    [Fact(DisplayName = "String should match regex (throws)")]
    public void StringShouldMatchRegexFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().MatchRegex("^foo$"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("match regular expression \"^foo$\"", exception.Message);
    }

    [Fact(DisplayName = "String should match regex")]
    public void StringShouldMatchRegex()
    {
        // Given
        var s = "fooBAR";

        // When
        s.Should().MatchRegex("foo");

        // Then
    }

    #endregion

    #region Should start with

    [Fact(DisplayName = "String should start with (throws)")]
    public void StringShouldStartWithFailed()
    {
        // Given
        var s = "fooBAR";

        // When
        var exception = Assert.Throws<XunitException>(() => s.Should().StartWith("BAR"));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("s", exception.Message);
        Assert.Contains("is \"fooBAR\"", exception.Message);
        Assert.Contains("start with \"BAR\"", exception.Message);
    }

    [Fact(DisplayName = "String should start with")]
    public void StringShouldStartWith()
    {
        // Given
        var s = "fooBAR";

        // When
        s.Should().StartWith("foo");

        // Then
    }

    #endregion
}
