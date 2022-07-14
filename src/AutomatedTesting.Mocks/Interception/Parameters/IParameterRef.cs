namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters;

using System.Collections.Generic;

/// <summary>
/// Non-generic feature interface for an <see cref="IInvocation"/> of a method that has ref parameters.
/// </summary>
public interface IParameterRef : IInvocationFeature, IParameterRefOrOut
{
    /// <summary>
    /// Gets the intercepted method's ref parameters.
    /// </summary>
    IEnumerable<Parameter> RefParameterCollection { get; }
}
