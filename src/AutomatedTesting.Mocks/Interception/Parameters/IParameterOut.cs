namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using System.Collections.Generic;

    /// <summary>
    /// Non-generic feature interface for an <see cref="IInvocation"/> of a method that has out parameters.
    /// </summary>
    public interface IParameterOut : IInvocationFeature, IParameterRefOrOut
    {
        /// <summary>
        /// Gets the intercepted method's out parameters.
        /// </summary>
        IEnumerable<Parameter> OutParameterCollection { get; }
    }
}