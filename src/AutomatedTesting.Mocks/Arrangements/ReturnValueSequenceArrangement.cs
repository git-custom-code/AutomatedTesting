namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Arrange a sequence of custom return values for intercepted method or property calls.
    /// </summary>
    /// <typeparam name="T"> The type of the arranged return value sequence. </typeparam>
    public sealed class ReturnValueSequenceArrangement<T> : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ReturnValueSequenceArrangement{T}"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method or property that is the target for this arrangement.
        /// </param>
        /// <param name="returnValueSequence"> The sequence of arranged return values. </param>
        public ReturnValueSequenceArrangement(MethodInfo signature, IList<T> returnValueSequence)
        {
            Signature = signature;
            ReturnValueSequence = returnValueSequence;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the sequence of arranged return values.
        /// </summary>
        private IList<T> ReturnValueSequence { get; }

        /// <summary>
        /// Gets the <see cref="ReturnValueSequence"/> index for the next value to be returned.
        /// </summary>
        private int SequenceIndex { get; set; } = 0;

        /// <summary>
        /// Gets the signature of the intercepted method or property that is the target for this arrangement.
        /// </summary>
        private MethodInfo Signature { get; }

        /// <summary>
        /// Gets a lightweight synchronization object for thread-safety.
        /// </summary>
        private object SyncLock { get; } = new object();

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
            if (invocation is FuncInvocation funcInvocation)
            {
                return funcInvocation.Signature == Signature;
            }
            else if (invocation is AsyncFuncInvocation asyncFuncInvocation)
            {
                return asyncFuncInvocation.Signature == Signature;
            }

            return false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Calls to '{Signature.Name}' should return '{ReturnValueSequence[SequenceIndex]}'";
        }

        /// <inheritdoc />
        public bool TryApplyTo(IInvocation invocation)
        {
            if (invocation is FuncInvocation funcInvocation)
            {
                if (funcInvocation.Signature == Signature)
                {
                    funcInvocation.ReturnValue = GetNextReturnValue();
                    return true;
                }
            }
            else if (invocation is AsyncFuncInvocation asyncFuncInvocation)
            {
                if (asyncFuncInvocation.Signature == Signature)
                {
                    var returnValue = GetNextReturnValue();
                    if (returnValue is Task asyncReturnValue)
                    {
                        asyncFuncInvocation.ReturnValue = asyncReturnValue;
                    }
                    else
                    {
                        asyncFuncInvocation.ReturnValue = Task.FromResult(returnValue);
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the next return value from the <see cref="ReturnValueSequence"/> in a thread-safe way.
        /// </summary>
        /// <returns> The next return value. </returns>
        private T GetNextReturnValue()
        {
            var returnValue = ReturnValueSequence[SequenceIndex];
            if (SequenceIndex < ReturnValueSequence.Count - 1)
            {
                lock (SyncLock)
                {
                    if (SequenceIndex < ReturnValueSequence.Count - 1)
                    {
                        SequenceIndex++;
                    }
                }
            }
            return returnValue;
        }

        #endregion
    }
}