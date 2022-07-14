namespace CustomCode.Analyzer.AutomatedTesting.Mocks.Tests;

using CustomCode.AutomatedTesting.Analyzer;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="CreateMockedGenericArgumentAnalyzer"/> type.
/// </summary>
public sealed class CreateMockedGenericArgumentAnalyzerTests
{
    [Fact(DisplayName = "CreateMockedGenericArgumentAnalyzer: report interface as error")]
    public async Task ReportInterfaceAsErrorAsync()
    {
        // Given
        var mocked = Analyzer.CreateMocked<CreateMockedGenericArgumentAnalyzer>();
        mocked.AddCode("public interface IFoo { }");
        mocked.AddCode(@"
                using CustomCode.AutomatedTesting.Mocks;

                public class Bar
                {
                    public void Execute()
                    {
                        var mocked = Mock.CreateMocked<IFoo>();
                    }
                }
            ");
        mocked.AddReferenceFor<CustomCode.AutomatedTesting.Mocks.IArrangement>();

        // When
        var diagnostics = await mocked.AnalyzeAsync();

        // Then
        Assert.NotNull(diagnostics);
        Assert.Single(diagnostics);
        Assert.Equal("AT1000", diagnostics.Single().Id);
        Assert.Contains("IFoo", diagnostics.Single().GetMessage());
    }

    [Fact(DisplayName = "CreateMockedGenericArgumentAnalyzer: don't report class as error")]
    public async Task DontReportClassAsErrorAsync()
    {
        // Given
        var mocked = Analyzer.CreateMocked<CreateMockedGenericArgumentAnalyzer>();
        mocked.AddCode("public class Foo { }");
        mocked.AddCode(@"
                using CustomCode.AutomatedTesting.Mocks;

                public class Bar
                {
                    public void Execute()
                    {
                        var mocked = Mock.CreateMocked<Foo>();
                    }
                }
            ");
        mocked.AddReferenceFor<CustomCode.AutomatedTesting.Mocks.IArrangement>();

        // When
        var diagnostics = await mocked.AnalyzeAsync();

        // Then
        Assert.NotNull(diagnostics);
        Assert.Empty(diagnostics);
    }
}
