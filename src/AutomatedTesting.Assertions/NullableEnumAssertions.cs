namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Assertions for nullable <see cref="Enum"/> values.
/// </summary>
/// <typeparam name="T"> The enumeration's type. </typeparam>
public sealed class NullableEnumAssertions<T> : IFluentInterface
    where T : struct, Enum
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="EnumAssertions{T}"/> type.
    /// </summary>
    /// <param name="nullableEnumeration"> The nullable enumeration to be validated. </param>
    public NullableEnumAssertions(Nullable<T> nullableEnumeration)
    {
        Context = new ValidationContext<Nullable<T>>();
        NullableEnumeration = nullableEnumeration;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<Nullable<T>> Context { get; }

    /// <summary>
    /// Gets the nullable enumeration to be validated.
    /// </summary>
    private Nullable<T> NullableEnumeration { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given nullable enumeration value equals an <paramref name="expected"/> value.
    /// </summary>
    /// <param name="expected"> The expected enumeration value. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(Nullable<T> expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (!Enum.Equals(NullableEnumeration, expected))
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableEnumeration}\"");
            var expectedText = FormattableString.Invariant($"to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable enumeration value has an <paramref name="expected"/> flag.
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
        if (NullableEnumeration == null || !NullableEnumeration.Value.HasFlag(expected))
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableEnumeration}\"");
            var expectedText = FormattableString.Invariant($"to have flag \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable enumeration value is null.
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
        if (NullableEnumeration != null)
        {
            var context = Context.GetCallerContext(testMethodName, null, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableEnumeration}\"");
            var expectedText = "to be null";
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    #endregion
}
