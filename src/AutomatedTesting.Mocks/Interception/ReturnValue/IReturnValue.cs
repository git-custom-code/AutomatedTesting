namespace CustomCode.AutomatedTesting.Mocks.Interception.ReturnValue
{
    /// <summary>
    /// Generic feature interface for an <see cref="IInvocation"/> of a method that has a non-void return value.
    /// </summary>
    public interface IReturnValue : IInvocationFeature
    {
        /// <summary>
        /// Gets or sets the return value of the intercepted method.
        /// </summary>
        object? ReturnValue { get; set; }
    }
}