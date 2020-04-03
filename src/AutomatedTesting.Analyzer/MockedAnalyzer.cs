namespace CustomCode.AutomatedTesting.Analyzer
{
    using Core;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Default implementation of the <see cref="IMockedAnalyzer{T}"/> interface.
    /// </summary>
    /// <typeparam name="T"> The type of the mocked analyzer. </typeparam>
    public sealed class MockedAnalyzer<T> : IMockedAnalyzer<T>
        where T : DiagnosticAnalyzer
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="MockedAnalyzer{T}"/> type.
        /// </summary>
        /// <param name="analyzer"> The analyzer to be tested. </param>
        /// <param name="project"> The <see cref="InMemoryProject"/> that contains the test code to be analyzed. </param>
        public MockedAnalyzer(T analyzer, IInMemoryProject project)
        {
            Analyzer = analyzer;
            Project = project;
        }

        /// <summary>
        /// Gets the <see cref="InMemoryProject"/> that contains the test code to be analyzed.
        /// </summary>
        private IInMemoryProject Project { get; }

        #endregion

        #region Data

        /// <summary>
        /// Gets the analyzer to be tested.
        /// </summary>
        private T Analyzer { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void AddCode(string sourceCode)
        {
            Project.Add(sourceCode);
        }

        /// <inheritdoc />
        public void AddReferenceFor<TReference>()
        {
            Project.AddReferenceFor<TReference>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Diagnostic>> AnalyzeAsync(CancellationToken token = default)
        {
            var compilation = await Project.CompileAsync(token);

            var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(Analyzer));
            var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
            var results = FilterDiagnostics(diagnostics, compilation.SyntaxTrees);
            return results;
        }

        /// <summary>
        /// Filter the diagnostic results of the <see cref="Analyzer"/> according to the specified <paramref name="syntaxTreeFilter"/>.
        /// </summary>
        /// <param name="diagnostics"> The analyzer's unfiltered diagnostic results. </param>
        /// <param name="syntaxTreeFilter"> The filter to be applied. </param>
        /// <returns> The filtered diagnostic results. </returns>
        private static IEnumerable<Diagnostic> FilterDiagnostics(
            IEnumerable<Diagnostic> diagnostics,
            IEnumerable<SyntaxTree> syntaxTreeFilter)
        {
            var results = new List<Diagnostic>();
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Location == Location.None || diagnostic.Location.IsInMetadata)
                {
                    results.Add(diagnostic);
                }
                else
                {
                    foreach (var tree in syntaxTreeFilter)
                    {
                        if (tree == diagnostic.Location.SourceTree)
                        {
                            results.Add(diagnostic);
                            break;
                        }
                    }
                }
            }
            return results;
        }

        #endregion
    }
}