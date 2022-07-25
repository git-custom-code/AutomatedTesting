namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Assertions for <see cref="Enum"/> values.
/// </summary>
/// <typeparam name="T"> The enumeration's type. </typeparam>
public sealed class EnumAssertions<T> : IFluentInterface
    where T : Enum
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="EnumAssertions{T}"/> type.
    /// </summary>
    /// <param name="enumeration"> The enumeration to be validated. </param>
    public EnumAssertions(T enumeration)
    {
        Context = new ValidationContext<T>();
        Enumeration = enumeration;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<T> Context { get; }

    /// <summary>
    /// Gets the enumeration to be validated.
    /// </summary>
    private T Enumeration { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given enumeration value equals an <paramref name="expected"/> value.
    /// </summary>
    /// <param name="expected"> The expected enumeration value. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(T expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (!Enum.Equals(Enumeration, expected))
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{Enumeration}\"");
            var expectedText = FormattableString.Invariant($"to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given enumeration value has an <paramref name="expected"/> flag.
    /// </summary>
    /// <param name="expected"> The expected flag. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void HaveFlag(T expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (!Enumeration.HasFlag(expected))
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{Enumeration}\"");
            var expectedText = FormattableString.Invariant($"to have flag \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    #endregion
}
