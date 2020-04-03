namespace CustomCode.AutomatedTesting.Analyzer
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that defines a mocked <see cref="DiagnosticAnalyzer"/> that can be executed on
    /// an <see cref="Core.InMemoryProject"/>.
    /// </summary>
    /// <typeparam name="T"> The type of the mocked analyzer. </typeparam>
    public interface IMockedAnalyzer<T>
        where T : DiagnosticAnalyzer
    {
        /// <summary>
        /// Add a new unit of c# <paramref name="sourceCode"/> to be analyzed.
        /// </summary>
        /// <param name="sourceCode"> The c# source code to be analyzed. </param>
        void AddCode(string sourceCode);

        /// <summary>
        /// Adds a reference to the assembly that contains the given type <typeparamref name="TReference"/>.
        /// </summary>
        /// <typeparam name="TReference">
        /// The type whose containing assembly should be added as a reference.
        /// </typeparam>
        void AddReferenceFor<TReference>();

        /// <summary>
        /// Run the mocked analyzer on the defined source code.
        /// </summary>
        /// <param name="token"> A token that can be used to cancel the analyzer process. </param>
        /// <returns> The analyzer's diagnostic results. </returns>
        Task<IEnumerable<Diagnostic>> AnalyzeAsync(CancellationToken token = default);
    }
}