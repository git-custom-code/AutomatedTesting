namespace CustomCode.AutomatedTesting.Configuration;

/// <summary>
/// Use implementations of this interface to display custom formatted validation error messages.
/// </summary>
public interface IMessageFormatter
{
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
    string FormatMessage(string? context, string actualValue, string expectation, string? reason);
}
