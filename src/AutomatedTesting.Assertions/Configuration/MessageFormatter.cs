namespace CustomCode.AutomatedTesting.Configuration;

using System;

/// <summary>
/// Basic implementation of the <see cref="IMessageFormatter"/> interface that is used to
/// format validation error messages.
/// </summary>
public sealed class MessageFormatter : IMessageFormatter
{
    #region Logic

    /// <summary>
    /// Creates a formatted validation message from the given parameters.
    /// </summary>
    /// <param name="context"> The caller context that contains information about the validated type. </param>
    /// <param name="actualValue"> The actual value that was validated by this instance. </param>
    /// <param name="expectation"> The expected value. </param>
    /// <param name="reason">
    /// An optional reason why the <paramref name="actualValue"/> should satisfy the <paramref name="expectation"/>.
    /// </param>
    /// <returns> A formatted and human readable validation message. </returns>
    public string FormatMessage(string? context, string actualValue, string expectation, string? reason)
    {
        var rn = Environment.NewLine;
        if (string.IsNullOrEmpty(reason))
        {
            if (string.IsNullOrEmpty(context))
            {
                return $"{rn}{actualValue}{rn}but was expected {expectation}";
            }
            return $"{rn}{context}{rn}{actualValue}{rn}but was expected {expectation}";
        }

        if (string.IsNullOrEmpty(context))
        {
            return $"{rn}{actualValue}{rn}but was expected {expectation}{rn}because {reason}";
        }

        return $"{rn}{context}{rn}{actualValue}{rn}but was expected {expectation}{rn}because {reason}";
    }

    #endregion
}
