namespace CustomCode.AutomatedTesting.Analyzer.Core
{
    using Microsoft.CodeAnalysis;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that defines an in-memory roslyn project for dynamic c# source code files.
    /// </summary>
    public interface IInMemoryProject
    {
        /// <summary>
        /// Add a new unit of c# <paramref name="sourceCode"/> to the project.
        /// </summary>
        /// <param name="sourceCode"> The c# source code to be added. </param>
        void Add(string sourceCode);

        /// <summary>
        /// Add a new or update an existing unit of c# <paramref name="sourceCode"/> to the project.
        /// </summary>
        /// <param name="name"> The unique name of the unit of code. </param>
        /// <param name="sourceCode"> The c# source code to be added or updated. </param>
        void AddOrUpdate(string name, string sourceCode);

        /// <summary>
        /// Add a new or update an existing unit of c# <paramref name="sourceCode"/> to the project.
        /// </summary>
        /// <param name="sourceCode"> The c# source code to be added or updated. </param>
        void AddOrUpdate(SourceCodeFile sourceCode);

        /// <summary>
        /// Adds a reference for the given <paramref name="assembly"/> to the project.
        /// </summary>
        /// <param name="assembly"> The assembly that should be added as a reference. </param>
        void AddReference(Assembly assembly);

        /// <summary>
        /// Adds a reference to the assembly that contains the given type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"> The type whose containing assembly should be added as a reference. </typeparam>
        void AddReferenceFor<T>();

        /// <summary>
        /// Asynchronously compile the in-memory project (using the roslyn compiler). 
        /// </summary>
        /// <param name="token"> A token that can be used to cancel the compilation process. </param>
        /// <returns> The compiled project. </returns>
        Task<Compilation> CompileAsync(CancellationToken token = default);
    }
}