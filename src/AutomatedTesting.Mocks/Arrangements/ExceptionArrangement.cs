namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using System;
    using System.Reflection;

    /// <summary>
    /// Arrange that an intercepted method or property call will throw an exception.
    /// </summary>
    public sealed class ExceptionArrangement : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ExceptionArrangement"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method or property that is the target for this arrangement.
        /// </param>
        /// <param name="exceptionFactory"> A factory that will create the exception instance to be thrown. </param>
        public ExceptionArrangement(MethodInfo signature, Func<Exception> exceptionFactory)
        {
            Signature = signature;
            ExceptionFactory = exceptionFactory;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a factory that will create the exception instance to be thrown.
        /// </summary>

        private Func<Exception> ExceptionFactory { get; }

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
           return invocation.Signature == Signature;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var exception = ExceptionFactory();
            return $"Calls to '{Signature.Name}' should throw an '{exception.GetType().Name}'";
        }

        /// <inheritdoc />
        public bool TryApplyTo(IInvocation invocation)
        {
            if (invocation.Signature == Signature)
            {
                throw ExceptionFactory();
            }

            return false;
        }

        #endregion
    }
}