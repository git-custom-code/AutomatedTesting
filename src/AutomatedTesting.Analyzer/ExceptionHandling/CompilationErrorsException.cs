namespace CustomCode.AutomatedTesting.Analyzer.ExceptionHandling;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Exception that is thrown when an <see cref="Core.InMemoryProject"/> was compiled with errors.
/// </summary>
public sealed class CompilationErrorsException : Exception
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="CompilationErrorsException"/> type.
    /// </summary>
    /// <param name="errors"> The compilation errors. </param>
    public CompilationErrorsException(IEnumerable<string> errors)
        : base($"In-memory project has {errors.Count()} compilation errors")
    {
        Errors = errors;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the compilation errors.
    /// </summary>
    public IEnumerable<string> Errors { get; }

    #endregion
}
