namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using Interception.Async;
    using Interception.ReturnValue;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Arrange a custom return value for an intercepted method or property call.
    /// </summary>
    /// <typeparam name="T"> The type of the arranged return value. </typeparam>
    public sealed class ReturnValueArrangement<T> : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ReturnValueArrangement{T}"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method or property that is the target for this arrangement.
        /// </param>
        /// <param name="returnValue"> The arranged return value. </param>
        public ReturnValueArrangement(MethodInfo signature, T returnValue)
        {
            Signature = signature;
            ReturnValue = returnValue;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the arranged return value.
        /// </summary>
        private T ReturnValue { get; }

        /// <summary>
        /// Gets the signature of the intercepted method or property that is the target for this arrangement.
        /// </summary>
        private MethodInfo Signature { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void ApplyTo(IInvocation invocation)
        {
            TryApplyTo(invocation);
        }

        /// <inheritdoc />
        public bool CanApplyTo(IInvocation invocation)
        {
            if (invocation.HasFeature<IReturnValue<T>>())
            {
                return invocation.Signature == Signature;
            }

            return false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Calls to '{Signature.Name}' should return '{ReturnValue}'";
        }

        /// <inheritdoc />
        public bool TryApplyTo(IInvocation invocation)
        {
            if (invocation.Signature == Signature)
            {
                if (invocation.HasFeature<IAsyncInvocation>())
                {
                    if (invocation.TryGetFeature<IAsyncInvocation<T>>(out var asyncFeature))
                    {
                        asyncFeature.AsyncReturnValue = ReturnValue;
                        return true;
                    }
                    else if (invocation.TryGetFeature<IAsyncInvocation<Task<T>>>(out var asyncTaskFeature))
                    {
                        asyncTaskFeature.AsyncReturnValue = Task.FromResult(ReturnValue);
                        return true;
                    }
                    else if (invocation.TryGetFeature<IAsyncInvocation<ValueTask<T>>>(out var asyncValueTaskFeature))
                    {
                        asyncValueTaskFeature.AsyncReturnValue = new ValueTask<T>(ReturnValue);
                        return true;
                    }
                    else if (invocation.TryGetFeature<IAsyncInvocation<IAsyncEnumerable<T>>>(out var asyncEnumerableFeature))
                    {
                        asyncEnumerableFeature.AsyncReturnValue = AsyncEnumerable.Create(_ =>
                            AsyncEnumerator.Create(
                                () => default,
                                () => ReturnValue,
                                () => default));
                        return true;
                    }
                }
                else if (invocation.TryGetFeature<IReturnValue<T>>(out var returnValueFeature))
                {

                    returnValueFeature.ReturnValue = ReturnValue;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}