namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters;

using System.Collections.Generic;

/// <summary>
/// Non-generic feature interface for an <see cref="IInvocation"/> of a method that has in(put) parameters.
/// </summary>
public interface IParameterIn : IInvocationFeature
{
    /// <summary>
    /// Gets the intercepted method's in(put) parameters.
    /// </summary>
    IEnumerable<Parameter> InputParameterCollection { get; }
}
