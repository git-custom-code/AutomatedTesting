namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncGenericTaskInvocation<TResult> : IAsyncInvocation<Task<TResult>>
    {
        #region Data

        /// <inheritdoc />
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
        public Task<TResult> ReturnValue { get; set; } = Task.FromResult(default(TResult));
#pragma warning restore CS8653

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.GenericTask;

        #endregion
    }
}