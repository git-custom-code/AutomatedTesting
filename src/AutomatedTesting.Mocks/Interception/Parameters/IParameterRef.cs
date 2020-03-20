namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using System.Collections.Generic;

    /// <summary>
    /// Non-generic feature interface for an <see cref="IInvocation"/> of a method that has ref parameters.
    /// </summary>
    public interface IParameterRef : IInvocationFeature
    {
        /// <summary>
        /// Gets the intercepted method's ref parameters.
        /// </summary>
        IEnumerable<Parameter> RefParameterCollection { get; }

        /// <summary>
        /// Get the value of the specified ref parameter.
        /// </summary>
        /// <typeparam name="T"> The type of the specified value. </typeparam>
        /// <param name="name"> The unique name of the parameter. </param>
        /// <returns> The ref parameter's current value. </returns>
        T GetValue<T>(string name)
    }
}