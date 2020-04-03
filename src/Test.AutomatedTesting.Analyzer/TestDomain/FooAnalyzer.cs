namespace CustomCode.AutomatedTesting.Analyzer.Tests.TestDomain
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;
    using System;
    using System.Collections.Immutable;

    /// <summary>
    /// Analyzer implementation that checks that no class is named "Foo".
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class FooAnalyzer : DiagnosticAnalyzer
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooAnalyzer"/> type.
        /// </summary>
        public FooAnalyzer()
        {
            Rule = new DiagnosticDescriptor(
                id: "FOO4711",
                title: "Title",
                messageFormat: "MessageFormat",
                category: "Category",
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true);
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

        // <inheritdoc />
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, new[] { SyntaxKind.ClassDeclaration });
        }

        /// <summary>
        /// Analyze that the name of no class is "Foo".
        /// </summary>
        /// <param name="context"> The roslyn context that contains the class declaration to be analyzed. </param>
        private void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classNode = (ClassDeclarationSyntax)context.Node;
            var symbol = context.SemanticModel.GetDeclaredSymbol(classNode) ?? throw new NullReferenceException();
            if (symbol.Name == "Foo")
            {
                var diagnostic = Diagnostic.Create(Rule, classNode.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }

        #endregion
    }
}