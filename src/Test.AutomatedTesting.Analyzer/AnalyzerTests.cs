namespace CustomCode.AutomatedTesting.Analyzer.Tests
{
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="Analyzer"/> type.
    /// </summary>
    public sealed class AnalyzerTests
    {
        [Fact(DisplayName = "Analyzer: create mocked analyzer")]
        public void CreateMocked()
        {
            // Given

            // When
            var mocked = Analyzer.CreateMocked<FooAnalyzer>();

            // Then
            Assert.NotNull(mocked);
        }
    }
}