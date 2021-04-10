namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using Interception.ReturnValue;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="ValueTask{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncGenericValueTaskInvocation<TResult> :
        IAsyncInvocation<ValueTask<TResult>>, IReturnValue<TResult>
    {
        #region Data

        /// <inheritdoc cref="IAsyncInvocation{T}" />
        public ValueTask<TResult> AsyncReturnValue { get; set; } = new ValueTask<TResult>();

        /// <inheritdoc cref="IReturnValue{T}" />
        TResult IReturnValue<TResult>.ReturnValue
        {
            get { return AsyncReturnValue.ConfigureAwait(false).GetAwaiter().GetResult(); }
#nullable disable
            set { AsyncReturnValue = new ValueTask<TResult>(value); }
#nullable restore
        }

        /// <inheritdoc cref="IReturnValue" />
        object? IReturnValue.ReturnValue
        {
            get { return (object?)((IReturnValue<TResult>)this).ReturnValue; }
#nullable disable
            set { ((IReturnValue<TResult>)this).ReturnValue = (TResult)value; }
#nullable restore
        }

        /// <inheritdoc cref="IAsyncInvocation" />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.GenericValueTask;

        #endregion
    }
}