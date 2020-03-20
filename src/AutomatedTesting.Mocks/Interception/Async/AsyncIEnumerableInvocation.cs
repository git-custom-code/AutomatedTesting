namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using Interception.ReturnValue;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="IAsyncEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"> The type of the asynchronous result value. </typeparam>
    public sealed class AsyncIEnumerableInvocation<T>
        : IAsyncInvocation<IAsyncEnumerable<T>>, IReturnValue<IEnumerable<T>>
    {
        #region Data

        /// <inheritdoc />
        public IAsyncEnumerable<T> AsyncReturnValue { get; set; } = AsyncEnumerable.Empty<T>();

        /// <inheritdoc />
        IEnumerable<T> IReturnValue<IEnumerable<T>>.ReturnValue
        {
            get { return AsyncEnumerable.ToEnumerable(AsyncReturnValue); }
            set
            {
                if (value == null)
                {
                    AsyncReturnValue = AsyncEnumerable.Empty<T>();
                }
                else
                {
                    var enumerator = value.GetEnumerator();
                    AsyncReturnValue = AsyncEnumerable.Create(_ =>
                        AsyncEnumerator.Create(
                            moveNextAsync: () =>
                                {
                                    var canMoveNext = enumerator.MoveNext();
                                    return new ValueTask<bool>(canMoveNext);
                                },
                            getCurrent: () => enumerator.Current,
                            disposeAsync: () =>
                                {
                                    enumerator.Dispose();
                                    return default;
                                }));
                }
            }
        }

        /// <inheritdoc />
        object? IReturnValue.ReturnValue
        {
            get { return (object?)((IReturnValue<IEnumerable<T>>)this).ReturnValue; }
            set { ((IReturnValue<IEnumerable<T>>)this).ReturnValue = value as IEnumerable<T>; }
        }

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.AsyncEnumerable;

        #endregion
    }
}