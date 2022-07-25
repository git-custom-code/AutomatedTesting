namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Inverse assertions for <see cref="bool"/>s.
/// </summary>
public sealed class BooleanInverseAssertions : IFluentInterface
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="BooleanInverseAssertions"/> type.
    /// </summary>
    /// <param name="boolean"> The boolean to be validated. </param>
    public BooleanInverseAssertions(bool boolean)
    {
        Context = new ValidationContext<bool>();
        Boolean = boolean;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<bool> Context { get; }

    /// <summary>
    /// Gets the boolean to be validated.
    /// </summary>
    private bool Boolean { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given boolean value does not equal an <paramref name="expected"/> value.
    /// </summary>
    /// <param name="expected"> The expected boolean value. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(bool expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (Boolean == expected)
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{Boolean}\"");
            var expectedText = FormattableString.Invariant($"not to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given boolean value does not equal "true".
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
        if (Boolean == true)
        {
            var context = Context.GetCallerContext(testMethodName, true, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{Boolean}\"");
            var expectedText = "not to be \"True\"";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given boolean value does not equal "false".
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
        if (Boolean == false)
        {
            var context = Context.GetCallerContext(testMethodName, true, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{Boolean}\"");
            var expectedText = "not to be \"False\"";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    #endregion
}
