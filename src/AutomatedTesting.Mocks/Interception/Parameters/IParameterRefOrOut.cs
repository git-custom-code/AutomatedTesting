namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Interface that defines common logic for the <see cref="IParameterRef"/> and
/// <see cref="IParameterOut"/> invocation features.
/// </summary>
public interface IParameterRefOrOut
{
    /// <summary>
    /// Get the value of the specified parameter.
    /// </summary>
    /// <typeparam name="T"> The type of the specified value. </typeparam>
    /// <param name="name"> The unique name of the parameter. </param>
    /// <returns> The parameter's current value. </returns>
    [return: MaybeNull]
    T GetValue<T>(string name);
}
