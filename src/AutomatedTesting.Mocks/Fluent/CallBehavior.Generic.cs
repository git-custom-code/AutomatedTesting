namespace CustomCode.AutomatedTesting.Mocks.Fluent
{
    using Arrangements;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Default implementation of the <see cref="ICallBehavior{TResult}"/> interface.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result value that is returned by the mocked method or property getter.
    /// </typeparam>
    public sealed class CallBehavior<TResult> : ICallBehavior<TResult>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CallBehavior{TResult}"/> type.
        /// </summary>
        /// <param name="arrangements"> The collection of arrangements that have been made for the associated mock. </param>
        /// <param name="signature"> The signature of the mocked method or property getter call. </param>
        public CallBehavior(IArrangementCollection arrangements, MethodInfo signature)
        {
            Arrangements = arrangements;
            Signature = signature;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the collection of arrangements that have been made for the associated mock.
        /// </summary>
        private IArrangementCollection Arrangements { get; }

        /// <summary>
        /// Gets the signature of the mocked method or property getter call.
        /// </summary>
        private MethodInfo Signature { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void Returns(TResult returnValue)
        {
            var arrangement = new ReturnValueArrangement<TResult>(Signature, returnValue);
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc />
        public void ReturnsSequence(params TResult[] returnValueSequence)
        {
            var sequence = new List<TResult>(returnValueSequence);
            var arrangement = new ReturnValueSequenceArrangement<TResult>(Signature, sequence);
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc />
        public void Throws<T>() where T : Exception, new()
        {
            var arrangement = new ExceptionArrangement(Signature, () => new T());
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc />
        public void Throws(Exception exception)
        {
            var arrangement = new ExceptionArrangement(Signature, () => exception);
            Arrangements.Add(arrangement);
        }

        #endregion
    }
}