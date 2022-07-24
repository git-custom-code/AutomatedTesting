namespace CustomCode.AutomatedTesting.Assertions;

using Configuration;
using Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

/// <summary>
/// Assertions for nullable number values.
/// </summary>
/// <typeparam name="T"> The concrete data type for the nullable number value (i.e. <see cref="int"/>). </typeparam>
public sealed class NullableNumberAssertions<T> : IFluentInterface
    where T : struct, INumber<T>
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="NullableNumberAssertions{T}"/> type.
    /// </summary>
    /// <param name="nullableNumber"> The nullable number to be validated. </param>
    public NullableNumberAssertions(Nullable<T> nullableNumber)
    {
        Context = new ValidationContext<Nullable<T>>();
        NullableNumber = nullableNumber;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the validation context.
    /// </summary>
    private ValidationContext<Nullable<T>> Context { get; }

    /// <summary>
    /// Gets the nullable number to be validated.
    /// </summary>
    private Nullable<T> NullableNumber { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Assert that a given nullable number equals an <paramref name="expected"/> number.
    /// </summary>
    /// <param name="expected"> The expected number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Be(Nullable<T> expected, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber != expected)
        {
            var context = Context.GetCallerContext(testMethodName, expected, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expectedText = FormattableString.Invariant($"to be \"{expected}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expectedText, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is between (or equal to) a specified <paramref name="minimum"/> and <paramref name="maximum"/>.
    /// </summary>
    /// <param name="minimum"> The allowed minimum value for the nullable number. </param>
    /// <param name="maximum"> The allowed maximum value for the nullable number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeBetween(T minimum, T maximum, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber < minimum || NullableNumber > maximum)
        {
            var context = Context.GetListCallerContext(testMethodName, new Nullable<T>[] { minimum, maximum }, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be between \"{minimum}\" and \"{maximum}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is greater than a specified <paramref name="minimum"/>.
    /// </summary>
    /// <param name="minimum"> The allowed minimum value for the nullable number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeGreaterThan(T minimum, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber <= minimum)
        {
            var context = Context.GetCallerContext(testMethodName, minimum, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be greater than \"{minimum}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is greater than or equal to a specified <paramref name="minimum"/>.
    /// </summary>
    /// <param name="minimum"> The allowed minimum value for the nullable number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeGreaterThanOrEqualTo(T minimum, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber < minimum)
        {
            var context = Context.GetCallerContext(testMethodName, minimum, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be greater than or equal to \"{minimum}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is less than a specified <paramref name="maximum"/>.
    /// </summary>
    /// <param name="maximum"> The allowed maximum value for the nullable number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeLessThan(T maximum, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber >= maximum)
        {
            var context = Context.GetCallerContext(testMethodName, maximum, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be less than \"{maximum}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is less than or equal to a specified <paramref name="maximum"/>.
    /// </summary>
    /// <param name="maximum"> The allowed maximum value for the nullable number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeLessThanOrEqualTo(T maximum, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber > maximum)
        {
            var context = Context.GetCallerContext(testMethodName, maximum, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be less than or equal to \"{maximum}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number matches one of the expected values.
    /// </summary>
    /// <param name="expectedNumbers"> A list of expected numbers. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeOneOf(IEnumerable<T> expectedNumbers, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        var value = NullableNumber;
        if (expectedNumbers?.Any(v => v == value) == false)
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be one of the following values: \"{string.Join("\", \"", expectedNumbers)}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number has a positive value.
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BePositive(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber < T.Zero)
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, "to be a positive number", because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number has a negative value.
    /// </summary>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeNegative(string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || NullableNumber >= T.Zero)
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, "to be a negative number", because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number equals an <paramref name="approximated"/> number within a given
    /// <paramref name="tolerance"/>.
    /// </summary>
    /// <param name="approximated"> The approximated value of the given nullable number. </param>
    /// <param name="tolerance"> The approximated value's tolerance. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void BeApproximately(T approximated, T tolerance, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (NullableNumber == null || T.Abs(NullableNumber.Value - approximated) > T.Abs(tolerance))
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to be approximately \"{approximated}\" (+-{tolerance})");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number fulfills the specified <paramref name="condition"/>.
    /// </summary>
    /// <param name="condition"> The condition that should be fulfilled by the given number. </param>
    /// <param name="because"> A reason why this assertion needs to be correct. </param>
    /// <param name="testMethodName"> Supplied by the compiler. </param>
    /// <param name="lineNumber"> Supplied by the compiler. </param>
    /// <param name="sourceCodePath"> Supplied by the compiler. </param>
    public void Fulfill(Expression<Func<Nullable<T>, bool>> condition, string? because = null,
#nullable disable
        [CallerMemberName] string testMethodName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string sourceCodePath = null)
#nullable restore
    {
        if (!condition.Compile()(NullableNumber))
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = FormattableString.Invariant($"to fulfill condition: \"{condition}\"");
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    /// <summary>
    /// Assert that a given nullable number is null.
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
        if (NullableNumber.HasValue)
        {
            var context = Context.GetCallerContext(testMethodName, T.Zero, sourceCodePath, lineNumber);
            var actual = FormattableString.Invariant($"is \"{NullableNumber}\"");
            var expected = "to be null";
            throw Context.GetFormattedException(testMethodName, context, actual, expected, because);
        }
    }

    #endregion
}
