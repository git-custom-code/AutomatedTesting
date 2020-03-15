namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncGenericValueTaskInvocation<TResult> : IAsyncInvocation<ValueTask<TResult>>
    {
        #region Data

        /// <inheritdoc />
        public ValueTask<TResult> ReturnValue { get; set; } = new ValueTask<TResult>();

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.GenericValueTask;

        #endregion
    }
}