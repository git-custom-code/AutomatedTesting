namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

/// <summary>
/// Assertions for <see cref="string"/>s.
/// </summary>
public sealed class StringAssertions : IFluentInterface
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="StringAssertions"/> type.
    /// </summary>
    /// <param name="string"> The string to be validated. </param>
    public StringAssertions(string? @string)
    {
        Context = new ValidationContext<string?>();
        String = @string;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<string?> Context { get; }

    /// <summary>
    /// Gets the string to be validated.
    /// </summary>
    private string? String { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given string equals an <paramref name="expected"/> string.
    /// </summary>
    /// <param name="expected"> The expected string. </param>
    /// <param name="ignoreCase"> False if the string comparison should be case sensitive, false otherwise. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(string expected, bool ignoreCase = false, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var comparisonMethod = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (string.Equals(String, expected, comparisonMethod) == false)
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string equals the empty string.
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeEmpty(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (String != string.Empty)
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = "to be empty";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string equals null.
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
        if (String != null)
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = "to be null";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string equals null or the empty string.
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeNullOrEmpty(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (!string.IsNullOrEmpty(String))
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = "to be null or empty";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string contains another (sub-)string.
    /// </summary>
    /// <param name="expected"> The expected value of a given (sub-)string. </param>
    /// <param name="ignoreCase"> False if the string comparison should be case sensitive, false otherwise. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Contain(string expected, bool ignoreCase = false, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var comparisonMethod = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (String == null || String.IndexOf(expected, comparisonMethod) == -1)
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to contain \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string ends with a sequence of characters.
    /// </summary>
    /// <param name="end"> The sequence of characters at the end of a given string. </param>
    /// <param name="ignoreCase"> False if the string comparison should be case sensitive, false otherwise. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void EndWith(string end, bool ignoreCase = false, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var comparisonMethod = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (String == null || string.IsNullOrEmpty(end) || !String.EndsWith(end, comparisonMethod))
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to end with \"{end}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string has the expected <paramref name="length"/>.
    /// </summary>
    /// <param name="length"> The expected length of a given string. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void HaveLength(int length, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (String == null || String.Length != length)
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\" (with a length of \"{String?.Length}\")");
            var expectedText = FormattableString.Invariant($"to have a length of \"{length}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string matches the specified (wildcard-)pattern.
    /// </summary>
    /// <param name="pattern"> The (wildcard-)pattern that should match. </param>
    /// <param name="ignoreCase"> False if the string comparison should be case sensitive, false otherwise. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Match(string pattern, bool ignoreCase = false, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        Regex regex;
        var regexPattern = pattern?.Replace("*", ".*") ?? ".*";
        regexPattern = $"^{regexPattern}$";
        if (ignoreCase)
        {
            regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
        }
        else
        {
            regex = new Regex(regexPattern);
        }

        if (String == null || !regex.IsMatch(String))
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to match pattern \"{pattern}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string matches the specified regular expression.
    /// </summary>
    /// <param name="regex"> The regex that should match. </param>
    /// <param name="regexOptions"> The options for the regular expression. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void MatchRegex(string regex, RegexOptions regexOptions = RegexOptions.None, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var expression = new Regex(regex ?? ".", regexOptions);
        if (String == null || !expression.IsMatch(String))
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to match regular expression \"{regex}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given string starts with a sequence of characters.
    /// </summary>
    /// <param name="start"> The sequence of characters at the start of a given string. </param>
    /// <param name="ignoreCase"> False if the string comparison should be case sensitive, false otherwise. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void StartWith(string start, bool ignoreCase = false, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var comparisonMethod = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (String == null || string.IsNullOrEmpty(start) || !String.StartsWith(start, comparisonMethod))
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{String}\"");
            var expectedText = FormattableString.Invariant($"to start with \"{start}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    #endregion
}
