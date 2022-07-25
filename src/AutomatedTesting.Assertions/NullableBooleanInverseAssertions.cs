namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Inverse assertions for nullable <see cref="bool"/>s.
/// </summary>
public sealed class NullableBooleanInverseAssertions : IFluentInterface
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="NullableBooleanInverseAssertions"/> type.
    /// </summary>
    /// <param name="nullableBoolean"> The nullable boolean to be validated. </param>
    public NullableBooleanInverseAssertions(bool? nullableBoolean)
    {
        Context = new ValidationContext<bool?>();
        NullableBoolean = nullableBoolean;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<bool?> Context { get; }

    /// <summary>
    /// Gets the nullable boolean to be validated.
    /// </summary>
    private bool? NullableBoolean { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given nullable boolean value does not equal an <paramref name="expected"/> value.
    /// </summary>
    /// <param name="expected"> The expected nullable boolean value. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(bool? expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableBoolean == expected)
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableBoolean}\"");
            var expectedText = FormattableString.Invariant($"not to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable boolean value does not equal "true".
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeTrue(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableBoolean == true)
        {
            var context = Context.GetCallerContext(testMethodName, true, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableBoolean}\"");
            var expectedText = "not to be \"True\"";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable boolean value does not equal "false".
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeFalse(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableBoolean == false)
        {
            var context = Context.GetCallerContext(testMethodName, true, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableBoolean}\"");
            var expectedText = "not to be \"False\"";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable boolean value is not null.
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeNull(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableBoolean == null)
        {
            var context = Context.GetCallerContext(testMethodName, true, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableBoolean}\"");
            var expectedText = "not to be null";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    #endregion
}
