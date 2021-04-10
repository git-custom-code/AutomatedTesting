namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="ValueTask"/>.
    /// </summary>
    public sealed class AsyncValueTaskInvocation : IAsyncInvocation<ValueTask>
    {
        #region Data

        /// <inheritdoc cref="IAsyncInvocation{T}" />
        public ValueTask AsyncReturnValue { get; set; } = default;

        /// <inheritdoc cref="IAsyncInvocation" />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.ValueTask;

        #endregion
    }
}