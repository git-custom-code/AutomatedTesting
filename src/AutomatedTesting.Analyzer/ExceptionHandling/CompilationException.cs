namespace CustomCode.AutomatedTesting.Analyzer.ExceptionHandling
{
    using System;

    /// <summary>
    /// Exception that is thrown when an <see cref="Core.InMemoryProject"/> could not be compiled.
    /// </summary>
    public sealed class CompilationException : Exception
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CompilationException"/> type.
        /// </summary>
        /// <param name="message"> The exception's error message. </param>
        public CompilationException(string message)
            : base (message)
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="CompilationException"/> type.
        /// </summary>
        /// <param name="message"> The exception's error message. </param>
        /// <param name="innerException"> The causing exception. </param>
        public CompilationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion
    }
}