namespace CustomCode.AutomatedTesting.Mocks.Interception.ReturnValue;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Generic feature interface for an <see cref="IInvocation"/> of a method that has a non-void return value.
/// </summary>
/// <typeparam name="T"> The type of the return value. </typeparam>
public interface IReturnValue<T> : IReturnValue
{
    /// <summary>
    /// Gets or sets the return value of the intercepted method.
    /// </summary>
    [AllowNull, MaybeNull]
    new T ReturnValue { get; set; }
}
