namespace CustomCode.Analyzer.AutomatedTesting.Mocks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Shared;
using System;
using System.Collections.Immutable;
using System.Linq;

/// <summary>
/// Analyzer that checks that "Mock.CreateMocked{T}" is only called with a generic type that is a class.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CreateMockedGenericArgumentAnalyzer : DiagnosticAnalyzer
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="CreateMockedGenericArgumentAnalyzer"/> type.
    /// </summary>
    public CreateMockedGenericArgumentAnalyzer()
    {
        Rule = new DiagnosticDescriptor(
            id: "AT1000",
            title: "Generic argument of CreateMocked<T> must be a class",
            messageFormat: "Generic argument \"{0}\" of CreateMocked<T> must be a class",
            category: Constants.Category,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "A type was used a generic argument T for Mock.CreateMocked<T> that was not a class");
        SupportedDiagnostics = ImmutableArray.Create(Rule);
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the analyzer's <see cref="DiagnosticDescriptor"/>.
    /// </summary>
    private DiagnosticDescriptor Rule { get; }

    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

    #endregion

    #region Logic

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, new[] { SyntaxKind.InvocationExpression });
    }

    /// <summary>
    /// Analyze that the name of no class is "Foo".
    /// </summary>
    /// <param name="context"> The roslyn context that contains the class declaration to be analyzed. </param>
    private void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
    {
        var invocation = (InvocationExpressionSyntax)context.Node;
        if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
        {
            var symbolInfo = context.SemanticModel.GetSymbolInfo(invocation);
            if (symbolInfo.Symbol is IMethodSymbol symbol)
            {
                if ((!Constants.TypeNameMock.Equals(symbol.ContainingType.Name, StringComparison.OrdinalIgnoreCase)) ||
                    (!Constants.MethodNameCreateMocked.Equals(symbol.Name, StringComparison.OrdinalIgnoreCase)) ||
                    (!Constants.NamespaceMocks.Equals(symbol.ContainingNamespace.ToString(), StringComparison.OrdinalIgnoreCase)) ||
                    symbol.IsStatic == false ||
                    symbol.IsGenericMethod == false ||
                    symbol.TypeArguments.Length != 1)
                {
                    return;
                }

                var genericType = symbol.TypeArguments[0];
                if (genericType.TypeKind != TypeKind.Class)
                {
                    var typeArgumentsNode = memberAccess
                        .DescendantNodes()
                        .SingleOrDefault(n => n.IsKind(SyntaxKind.TypeArgumentList));

                    var genericTypeNode = typeArgumentsNode
                        .DescendantNodes()
                        .SingleOrDefault(n => n.IsKind(SyntaxKind.IdentifierName));

                    var diagnostic = Diagnostic.Create(
                        Rule,
                        genericTypeNode.GetLocation(),
                        genericType.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    #endregion
}
