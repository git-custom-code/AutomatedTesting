namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    /// <summary>
    /// Non-generic feature interface for an <see cref="IInvocation"/> of an asynchrononous method.
    /// </summary>
    public interface IAsyncInvocation : IInvocationFeature
    {
        /// <summary>
        /// Gets the type of the asynchronous method invocation (see <see cref="AsyncInvocationType"/> for more details).
        /// </summary>
        AsyncInvocationType Type { get; }
    }
}