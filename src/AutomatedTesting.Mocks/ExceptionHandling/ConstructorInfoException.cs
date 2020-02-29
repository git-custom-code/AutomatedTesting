namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Exception that is thrown whenever a <see cref="ConstructorInfo"/> could not be found via reflection.
    /// </summary>
    public sealed class ConstructorInfoException : ReflectionException
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ConstructorInfoException"/> type.
        /// </summary>
        /// <param name="sourceType"> The <see cref="Type"/> whose constructor could not be found. </param>
        public ConstructorInfoException(Type sourceType)
            : base($"Unable to find a ctor for type '{sourceType.Name}' via reflection", sourceType)
        { }

        #endregion
    }
}