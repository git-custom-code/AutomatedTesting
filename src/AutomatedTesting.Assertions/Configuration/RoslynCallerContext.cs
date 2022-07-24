namespace CustomCode.AutomatedTesting.Configuration;

using Assertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// Basic implementation of the <see cref="ICallerContext"/> interface that uses roslyn to parse
/// source code files in order to obtain information about the type (name) and property (name) that
/// was used to call a validation extension method on.
/// </summary>
public sealed class RoslynCallerContext : ICallerContext
{
    #region Logic

    /// <summary>
    /// Gets the caller context for a given validation method.
    /// </summary>
    /// <param name="expected"> The expected value specified as validation method parameter. </param>
    /// <param name="testMethodName">
    /// The name of the test method that contains the call to the validation method.
    /// </param>
    /// <param name="validationMethodName"> The name of the called validation method. </param>
    /// <param name="lineNumber"> The line number that contains the call to the validation method. </param>
    /// <param name="sourceCodePath">
    /// The path to the source code file that contains the call to the validation method.
    /// </param>
    /// <returns> The parsed caller context or null. </returns>
    public string? GetCallerContext<T>(T expected, string testMethodName, string validationMethodName,
        int lineNumber, string sourceCodePath)
    {
        using var sourceCode = File.OpenRead(sourceCodePath);
        var syntaxTree = CSharpSyntaxTree.ParseText(
            SourceText.From(sourceCode),
            path: sourceCodePath);
        var root = syntaxTree.GetRoot();
        var lineSpanWithValidatorCall = syntaxTree
            .GetText()
            .Lines
            .SingleOrDefault(l => l.LineNumber == lineNumber - 1)
            .Span;
        var nodeWithValidatorCall = root
            .DescendantNodes(lineSpanWithValidatorCall)
            .FirstOrDefault(n =>
            {
                if (lineSpanWithValidatorCall.Contains(n.Span))
                {
                    if (n is MemberAccessExpressionSyntax mae)
                    {
                        if (mae.Name?.Identifier.Text == validationMethodName)
                        {
                            return true;
                        }
                    }
                }
                return false;
            });

        if (nodeWithValidatorCall != null)
        {
            if (!HasMatchingExpectedArgumentValues(expected, nodeWithValidatorCall))
            {
                return null;
            }

            var callerNameBuilder = new StringBuilder();
            if (nodeWithValidatorCall.DescendantNodes().FirstOrDefault() is InvocationExpressionSyntax invocation &&
                invocation.Expression is MemberAccessExpressionSyntax memberAccess)
            {
                var member = memberAccess.DescendantNodes().FirstOrDefault();
                var method = memberAccess.DescendantNodes().LastOrDefault();
                if (member is LiteralExpressionSyntax literal &&
                    method is IdentifierNameSyntax methodName &&
                    methodName.Identifier.Text == nameof(ShouldExtensions.Should))
                {
                    return $"Constant {literal}";
                }
            }

            var identifier = nodeWithValidatorCall
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(i => i?.Parent?.Parent?.IsKind(SyntaxKind.GenericName) != true);
            foreach (var i in identifier)
            {
                if (validationMethodName != i.Identifier.Text &&
                   nameof(ShouldExtensions.Should) != i.Identifier.Text)
                {
                    callerNameBuilder.AppendFormat("{0}.", i.Identifier.Text);
                }
            }

            return callerNameBuilder.ToString(0, callerNameBuilder.Length - 1);
        }

        return null;
    }

    /// <summary>
    /// Check that a validator method call matches with the expected value(s).
    /// Note: This is necessary is 2 validator calls are within the same line.
    /// </summary>
    /// <typeparam name="T"> The type of the excepted value(s). </typeparam>
    /// <param name="expected"> The expected value(s) to be checked. </param>
    /// <param name="nodeWithValidatorCall"> The syntax node for the validator method call. </param>
    /// <returns> True if the expected value(s) were found, false otherwise. </returns>
    private bool HasMatchingExpectedArgumentValues<T>(T expected, SyntaxNode nodeWithValidatorCall)
    {
        var signature = nodeWithValidatorCall.Parent as InvocationExpressionSyntax;
        if (expected is not string && expected is IEnumerable expectedList)
        {
            var index = 0;
            foreach (var expectedValue in expectedList)
            {
                var parameter = signature?.ArgumentList?.Arguments.ElementAt(index);
                if (expected != null && parameter != null && parameter.Expression is LiteralExpressionSyntax literal)
                {
                    var elementType = expectedList.GetType().GetElementType();
                    if (elementType == null)
                    {
                        return false;
                    }

                    elementType = Nullable.GetUnderlyingType(elementType) ?? elementType;
                    var value = Convert.ChangeType(literal.Token.Value, elementType);
                    if (!Equals(value, expectedValue))
                    {
                        return false;
                    }
                }
                ++index;
            }
        }
        else
        {
            var parameter = signature?.ArgumentList?.Arguments.FirstOrDefault();
            if (!Equals(expected, default(T)) && parameter != null && parameter.Expression is LiteralExpressionSyntax literal)
            {
                var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                var value = Convert.ChangeType(literal.Token.Value, type);
                if (!Equals(value, expected))
                {
                    return false;
                }
            }
        }

        return true;
    }

    #endregion
}
