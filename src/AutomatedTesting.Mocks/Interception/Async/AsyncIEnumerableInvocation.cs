namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncIEnumerableInvocation<T> : IAsyncInvocation<IAsyncEnumerable<T>>
    {
        #region Data

        /// <inheritdoc />
        public IAsyncEnumerable<T> ReturnValue { get; set; } = AsyncEnumerable.Empty<T>();

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.AsyncEnumerable;

        #endregion
    }
}