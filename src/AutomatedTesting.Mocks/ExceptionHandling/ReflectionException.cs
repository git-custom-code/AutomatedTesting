namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling;

using System;

/// <summary>
/// Base type for exceptions that are caused by reflection.
/// </summary>
public abstract class ReflectionException : Exception
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ReflectionException"/> type.
    /// </summary>
    /// <param name="message"> The message that describes the error that has occured. </param>
    /// <param name="sourceType"> The <see cref="Type"/> that has caused the exception to be thrown. </param>
    protected ReflectionException(string message, Type sourceType)
        : base(message)
    {
        SourceType = sourceType;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the <see cref="Type"/> that has caused the reflection to be thrown.
    /// </summary>
    public Type SourceType { get; }

    #endregion
}
