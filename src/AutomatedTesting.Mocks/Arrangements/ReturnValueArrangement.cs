namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
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
            if (invocation is IHasReturnValue || invocation is IHasAsyncReturnValue)
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
            if (invocation is IHasReturnValue returnValueInvocation)
            {
                if (invocation.Signature == Signature)
                {
                    returnValueInvocation.ReturnValue = ReturnValue;
                    return true;
                }
            }
            else if (invocation is IHasAsyncReturnValue asyncReturnValueInvocation)
            {
                if (invocation.Signature == Signature)
                {
                    if (ReturnValue is Task asyncReturnValue)
                    {
                        asyncReturnValueInvocation.ReturnValue = asyncReturnValue;
                    }
                    else
                    {
                        asyncReturnValueInvocation.ReturnValue = Task.FromResult(ReturnValue);
                    }
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}