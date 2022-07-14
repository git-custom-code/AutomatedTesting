namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling;

using System;
using System.Reflection;

/// <summary>
/// Exception that is thrown whenever a <see cref="MethodInfo"/> invocation was intercepted by a
/// <see cref="Interception.StrictMockInterceptor"/> that has no <see cref="IArrangement"/> setup.
/// </summary>
public sealed class MissingArrangementException : Exception
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MissingArrangementException"/> type.
    /// </summary>
    /// <param name="message"> The message that details the missing arrangment and <paramref name="invokedSignature"/>. </param>
    /// <param name="invokedSignature"> The signature of the invoked method or property without arrangment. </param>
    public MissingArrangementException(string message, MethodInfo invokedSignature)
        : base (message)
    {
        InvokedSignature = invokedSignature;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the signature of the invoked method or property without arrangment.
    /// </summary>
    public MethodInfo InvokedSignature { get; }

    #endregion
}
