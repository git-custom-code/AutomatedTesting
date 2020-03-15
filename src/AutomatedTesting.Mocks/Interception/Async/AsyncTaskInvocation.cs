namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="Task"/>.
    /// </summary>
    public sealed class AsyncTaskInvocation : IAsyncInvocation<Task>
    {
        #region Data

        /// <inheritdoc />
        public Task ReturnValue { get; set; } = Task.CompletedTask;

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.Task;

        #endregion
    }
}