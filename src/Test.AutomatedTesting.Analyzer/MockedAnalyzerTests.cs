namespace CustomCode.AutomatedTesting.Analyzer.Tests
{
    using Core;
    using TestDomain;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="MockedAnalyzer{T}"/> type.
    /// </summary>
    public sealed class MockedAnalyzerTests
    {
        [Fact(DisplayName = "MockedAnalyzer: analyze async (empty solution)")]
        public async Task AnalyzeEmptySolutionAsync()
        {
            // Given
            var analyzer = new FooAnalyzer();
            var project = new InMemoryProject();
            var mocked = new MockedAnalyzer<FooAnalyzer>(analyzer, project);

            // When
            var diagnostics = await mocked.AnalyzeAsync();

            // Then
            Assert.NotNull(diagnostics);
            Assert.Empty(diagnostics);
        }

        [Fact(DisplayName = "MockedAnalyzer: analyze async")]
        public async Task AnalyzeAsync()
        {
            // Given
            var analyzer = new FooAnalyzer();
            var project = new InMemoryProject();
            var mocked = new MockedAnalyzer<FooAnalyzer>(analyzer, project);

            // When
            mocked.AddCode("public class Foo { }");
            var diagnostics = await mocked.AnalyzeAsync();

            // Then
            Assert.NotNull(diagnostics);
            Assert.Single(diagnostics);
        }
    }
}