namespace CustomCode.AutomatedTesting.Analyzer.Core.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="InMemoryProject"/> type.
    /// </summary>
    public sealed class InMemoryProjectTests
    {
        [Fact(DisplayName = "InMemoryProject: add class")]
        public async Task AddClassAsync()
        {
            // Given
            var project = new InMemoryProject();

            // When
            project.Add("public class Foo { }");
            var compilation = await project.CompileAsync();

            // Then
            Assert.NotNull(compilation);
            Assert.True(compilation.ContainsSymbolsWithName("Foo"));
        }

        [Fact(DisplayName = "InMemoryProject: add or update class")]
        public async Task AddOrUpdateClassAsync()
        {
            // Given
            var project = new InMemoryProject();

            // When
            project.AddOrUpdate("Foo.cs", "public class Foo { }");
            var compilation = await project.CompileAsync();

            // Then
            Assert.NotNull(compilation);
            Assert.True(compilation.ContainsSymbolsWithName("Foo"));
        }

        [Fact(DisplayName = "InMemoryProject: add or update source code")]
        public async Task AddOrUpdateSourceCodeAsync()
        {
            // Given
            var project = new InMemoryProject();
            var code = new SourceCodeFile("Foo.cs", "public class Foo { }");

            // When
            project.AddOrUpdate(code);
            var compilation = await project.CompileAsync();

            // Then
            Assert.NotNull(compilation);
            Assert.True(compilation.ContainsSymbolsWithName("Foo"));
        }

        [Fact(DisplayName = "InMemoryProject: add reference")]
        public async Task AddReferenceAsync()
        {
            // Given
            var project = new InMemoryProject();

            // When
            project.AddReference(typeof(InMemoryProject).Assembly);
            var compilation = await project.CompileAsync();

            // Then
            Assert.NotNull(compilation);
            Assert.Contains(compilation.ReferencedAssemblyNames, a => a.Name == typeof(InMemoryProject).Assembly.GetName().Name);
        }

        [Fact(DisplayName = "InMemoryProject: add reference for type")]
        public async Task AddReferenceForTypeAsync()
        {
            // Given
            var project = new InMemoryProject();

            // When
            project.AddReferenceFor<InMemoryProject>();
            var compilation = await project.CompileAsync();

            // Then
            Assert.NotNull(compilation);
            Assert.Contains(compilation.ReferencedAssemblyNames, a => a.Name == typeof(InMemoryProject).Assembly.GetName().Name);
        }
    }
}