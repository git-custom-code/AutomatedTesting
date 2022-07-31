namespace CustomCode.AutomatedTesting.Assertions.Tests;

using System.Reflection;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Automated tests for the <see cref="NullableEnumInverseAssertions{T}"/> type.
/// </summary>
public sealed class NullableEnumInverseAssertionTests
{
    #region Should not be

    [Fact(DisplayName = "Nullable enum should not be (throws)")]
    public void NullableEnumShouldNotBeExpectedValueFailed()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.ShouldNot().Be(BindingFlags.Public));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Public\"", exception.Message);
        Assert.Contains("not to be \"Public\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should not be")]
    public void NullableEnumShouldNotBeExpectedValue()
    {
        // Given
        BindingFlags? enumeration = null;

        // When
        enumeration.ShouldNot().Be(BindingFlags.NonPublic);

        // Then
    }

    #endregion

    #region Should not have flag

    [Fact(DisplayName = "Nullable enum should not have flag (throws)")]
    public void NullableEnumShouldNotHaveFlagFailed()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.ShouldNot().HaveFlag(BindingFlags.Public));

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"Instance, Public\"", exception.Message);
        Assert.Contains("not to have flag \"Public\"", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should not have flag")]
    public void NullableEnumShouldNotHaveFlag()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public | BindingFlags.Instance;

        // When
        enumeration.ShouldNot().HaveFlag(BindingFlags.NonPublic);

        // Then
    }

    #endregion

    #region Should not be null

    [Fact(DisplayName = "Nullable enum should not be null (throws)")]
    public void NullableEnumShouldNotBeNullFailed()
    {
        // Given
        BindingFlags? enumeration = null;

        // When
        var exception = Assert.Throws<XunitException>(() => enumeration.ShouldNot().BeNull());

        // Then
        Assert.NotNull(exception);
        Assert.Contains("enumeration", exception.Message);
        Assert.Contains("is \"\"", exception.Message);
        Assert.Contains("not to be null", exception.Message);
    }

    [Fact(DisplayName = "Nullable enum should not be null")]
    public void NullableEnumShouldNotBeNull()
    {
        // Given
        BindingFlags? enumeration = BindingFlags.Public;

        // When
        enumeration.ShouldNot().BeNull();

        // Then
    }

    #endregion
}
