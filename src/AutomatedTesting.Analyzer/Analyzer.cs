namespace CustomCode.AutomatedTesting.Analyzer;

using Core;
using Microsoft.CodeAnalysis.Diagnostics;

/// <summary>
/// Static entry point that allows the creation of <see cref="IMockedAnalyzer{T}"/> instances.
/// </summary>
public static class Analyzer
{
    #region Logic

    /// <summary>
    /// Create a new mocked <see cref="DiagnosticAnalyzer"/> to be tested.
    /// </summary>
    /// <typeparam name="T"> The type of the analyzer to be tested. </typeparam>
    /// <returns> The created <see cref="IMockedAnalyzer{T}"/> instance. </returns>
    public static IMockedAnalyzer<T> CreateMocked<T>()
        where T : DiagnosticAnalyzer, new()
    {
        var analyzer = new T();
        var project = new InMemoryProject();
        return new MockedAnalyzer<T>(analyzer, project);
    }

    #endregion
}
