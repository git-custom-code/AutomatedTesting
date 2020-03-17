namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using Interception.ReturnValue;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="Task{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncGenericTaskInvocation<TResult> :
        IAsyncInvocation<Task<TResult>>, IReturnValue<TResult>
    {
        #region Data

        /// <inheritdoc />
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
        public Task<TResult> AsyncReturnValue { get; set; } = Task.FromResult(default(TResult));
#pragma warning restore CS8653

        /// <inheritdoc />
        TResult IReturnValue<TResult>.ReturnValue
        {
            get { return AsyncReturnValue.ConfigureAwait(false).GetAwaiter().GetResult(); }
            set { AsyncReturnValue = Task.FromResult(value); }
        }

        /// <inheritdoc />
        object? IReturnValue.ReturnValue
        {
            get { return (object?)((IReturnValue<TResult>)this).ReturnValue; }
#pragma warning disable CS8601 // Possible null reference assignment.
            set { ((IReturnValue<TResult>)this).ReturnValue = (TResult)value; }
#pragma warning restore CS8601
        }

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.GenericTask;

        #endregion
    }
}