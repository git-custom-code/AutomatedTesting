namespace CustomCode.AutomatedTesting.Analyzer.Core;

using ExceptionHandling;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Default implementation of the <see cref="IInMemoryProject"/> interface.
/// </summary>
public sealed class InMemoryProject : IInMemoryProject
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="InMemoryProject"/> type.
    /// </summary>
    public InMemoryProject()
    {
        Project = new Lazy<Project>(CreateProject, true);
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the interal in-memory roslyn project.
    /// </summary>
    private Lazy<Project> Project { get; set; }

    /// <summary>
    /// Gets the project's references.
    /// </summary>
    private HashSet<Assembly> References { get; } = new HashSet<Assembly>();

    /// <summary>
    /// Gets the project's source code files.
    /// </summary>
    private ConcurrentDictionary<string, SourceCodeFile> Sources { get; }
        = new ConcurrentDictionary<string, SourceCodeFile>();

    /// <summary>
    /// Gets a lightweigth synchronization object for thread-safety.
    /// </summary>
    private object SyncLock { get; } = new object();

    #endregion

    #region Logic

    /// <inheritdoc />
    public void Add(string sourceCode)
    {
        var fileName = $"TempFile{Sources.Count + 1}";
        AddOrUpdate(new SourceCodeFile(fileName, sourceCode));
    }

    /// <inheritdoc />
    public void AddOrUpdate(string name, string sourceCode)
    {
        AddOrUpdate(new SourceCodeFile(name, sourceCode));
    }

    /// <inheritdoc />
    public void AddOrUpdate(SourceCodeFile sourceCode)
    {
        if (Project.IsValueCreated)
        {
            lock (SyncLock)
            {
                Sources.AddOrUpdate(sourceCode.Name, sourceCode, (key, value) => sourceCode);
                Project = new Lazy<Project>(CreateProject, true);
            }
        }
        else
        {
            Sources.AddOrUpdate(sourceCode.Name, sourceCode, (key, value) => sourceCode);
        }
    }

    /// <inheritdoc />
    public void AddReference(Assembly assembly)
    {
        if (Project.IsValueCreated)
        {
            lock (SyncLock)
            {
                if (!References.Contains(assembly))
                {
                    References.Add(assembly);
                }
                Project = new Lazy<Project>(CreateProject, true);
            }
        }
        else
        {
            lock (SyncLock)
            {
                if (!References.Contains(assembly))
                {
                    References.Add(assembly);
                }
            }
        }
    }

    /// <inheritdoc />
    public void AddReferenceFor<T>()
    {
        AddReference(typeof(T).GetTypeInfo().Assembly);
    }

    /// <inheritdoc />
    public async Task<Compilation> CompileAsync(CancellationToken token = default)
    {
        try
        {
            var compilation = await Project.Value.GetCompilationAsync(token);
            if (compilation == null)
            {
                throw new CompilationException("Unable to compile in-memory project");
            }

            var errors = compilation.GetDiagnostics().Where(d =>
                {
                    return d.Severity == DiagnosticSeverity.Error &&
                           d.Location.IsInSource;
                });
            if (errors.Any())
            {
                throw new CompilationErrorsException(errors.Select(e => e.GetMessage()));
            }

            return compilation;
        }
        catch (Exception e)
        {
            throw new CompilationException($"Unable to compile in-memory project: {e.Message}", e);
        }
    }

    /// <summary>
    /// Create a dynamic in-memory project from the given <see cref="Sources"/> and <see cref="References"/>.
    /// </summary>
    /// <returns> A dynamic in-memory project from the given <see cref="Sources"/> and <see cref="References"/>. </returns>
    private Project CreateProject()
    {
        var workspace = new AdhocWorkspace();
        var solution = workspace.CurrentSolution;

        var testProjectName = "TestProject";
        var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location) ?? throw new FileNotFoundException();
        var project = solution.AddProject(testProjectName, testProjectName, LanguageNames.CSharp);
        project = project.AddMetadataReferences(new[]
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(CSharpCompilation).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Compilation).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll"))
            });

        foreach (var reference in References)
        {
            project = project.AddMetadataReference(MetadataReference.CreateFromFile(reference.Location));
        }

        foreach (var source in Sources.Values)
        {
            var document = project.AddDocument(source.Name, SourceText.From(source.Code));
            project = document.Project;
        }

        return project;
    }

    #endregion
}
