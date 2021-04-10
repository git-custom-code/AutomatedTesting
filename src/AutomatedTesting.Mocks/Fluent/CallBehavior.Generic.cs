namespace CustomCode.AutomatedTesting.Mocks
{
    using Arrangements;
    using ExceptionHandling;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Default implementation of the <see cref="ICallBehavior{TResult}"/> interface.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result value that is returned by the mocked method or property getter.
    /// </typeparam>
    public sealed class CallBehavior<TResult> : CallBehaviorBase, ICallBehavior<TResult>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CallBehavior{TResult}"/> type.
        /// </summary>
        /// <param name="arrangements"> The collection of arrangements that have been made for the associated mock. </param>
        /// <param name="signature"> The signature of the mocked method or property getter call. </param>
        public CallBehavior(IArrangementCollection arrangements, MethodInfo signature)
            : base(arrangements, signature)
        { }

        #endregion

        #region Logic

        /// <inheritdoc cref="ICallBehavior{TResult}" />
        public void Returns(TResult returnValue)
        {
            var arrangement = new ReturnValueArrangement<TResult>(Signature, returnValue);
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc cref="ICallBehavior{TResult}" />
        public void ReturnsSequence(params TResult[] returnValueSequence)
        {
            Ensures.NotNull(returnValueSequence, nameof(returnValueSequence));

            var sequence = new List<TResult>(returnValueSequence);
            var arrangement = new ReturnValueSequenceArrangement<TResult>(Signature, sequence);
            Arrangements.Add(arrangement);
        }

        #endregion
    }
}