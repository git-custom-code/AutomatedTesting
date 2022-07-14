namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling;

using System;
using System.Reflection;

/// <summary>
/// Exception that is thrown whenever a <see cref="PropertyInfo"/> could not be found via reflection.
/// </summary>
public sealed class PropertyInfoException : ReflectionException
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="PropertyInfoException"/> type.
    /// </summary>
    /// <param name="sourceType"> The <see cref="Type"/> whose property could not be found. </param>
    /// <param name="propertyName"> The name of the property that could not be found. </param>
    public PropertyInfoException(Type sourceType, string propertyName)
        : base($"Unable to find property '{sourceType.Name}.{propertyName}' via reflection", sourceType)
    {
        PropertyName = propertyName;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the name of the property that could not be found.
    /// </summary>
    public string PropertyName { get; }

    #endregion
}
