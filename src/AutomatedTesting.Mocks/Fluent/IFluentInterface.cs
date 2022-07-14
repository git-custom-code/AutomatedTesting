namespace CustomCode.AutomatedTesting.Mocks.Fluent;

using System;
using System.ComponentModel;

/// <summary>
/// Interface that is used to build fluent interfaces by hiding methods declared
/// by <see cref="object"/> from IntelliSense.
/// </summary>
/// <remarks>
/// Code that consumes implementations of this interface should expect one of two
/// things: When referencing the interface from within the same solution (project
/// reference), you will still see the methods this interface is meant to hide. When
/// referencing the interface through the compiled output assembly (external reference),
/// the standard Object methods will be hidden as intended. When using Resharper,
/// be sure to configure it to respect the attribute: Options, go to Environment
/// | IntelliSense | Completion Appearance and check "Filter members by [EditorBrowsable]
/// attribute".
/// See https://kzu.github.io/IFluentInterface for more information.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IFluentInterface
{
    /// <summary>
    /// Redeclaration that hides the <see cref="object.Equals(object?)"/> method from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object? other);

    /// <summary>
    /// Redeclaration that hides the <see cref="object.GetHashCode"/> method from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    /// <summary>
    /// Redeclaration that hides the <see cref="object.GetType"/> method from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1716 // Identifiers should not match keywords
    Type GetType();
#pragma warning restore CA1716

    /// <summary>
    /// Redeclaration that hides the <see cref="object.ToString"/> method from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    string? ToString();
}
