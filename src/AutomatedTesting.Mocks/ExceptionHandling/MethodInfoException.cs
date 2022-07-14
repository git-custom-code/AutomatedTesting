namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling;

using System;
using System.Reflection;

/// <summary>
/// Exception that is thrown whenever a <see cref="MethodInfo"/> could not be found via reflection.
/// </summary>
public sealed class MethodInfoException : ReflectionException
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MethodInfoException"/> type.
    /// </summary>
    /// <param name="sourceType"> The <see cref="Type"/> whose method could not be found. </param>
    /// <param name="methodName"> The name of the method that could not be found. </param>
    public MethodInfoException(Type sourceType, string methodName)
        : base($"Unable to find method '{sourceType.Name}.{methodName}' via reflection", sourceType)
    {
        MethodName = methodName;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the name of the method that could not be found.
    /// </summary>
    public string MethodName { get; }

    #endregion
}
