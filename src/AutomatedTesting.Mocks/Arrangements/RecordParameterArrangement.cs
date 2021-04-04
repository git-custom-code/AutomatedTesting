namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using Interception.Parameters;
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Arrange that an intercepted method or property setter call will automatically record the received input parameters.
    /// </summary>
    /// <typeparam name="T"> The type that contains the recorded input parameters (usually a <see cref="ValueTuple"/>). </typeparam>
    public sealed class RecordParameterArrangement<T> : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="RecordParameterArrangement{T}"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method or property setter that is the target for this arrangement.
        /// </param>
        /// <param name="recordedCalls"> A thread-safe collection that is used to record the input parameters. </param>
        /// <param name="tryRecord">
        /// A delegate that tries to convert the intercepted input parameters to an instance of type <typeparamref name="T"/>.
        /// </param>
        public RecordParameterArrangement(
            MethodInfo signature,
            ConcurrentQueue<T> recordedCalls,
            Func<(Type type, object? value)[], (bool, T)> tryRecord)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            RecordedCalls = recordedCalls ?? throw new ArgumentNullException(nameof(recordedCalls));
            TryRecord = tryRecord ?? throw new ArgumentNullException(nameof(tryRecord));
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a thread-safe collection that is used to record the input parameters.
        /// </summary>
        private ConcurrentQueue<T> RecordedCalls { get; }

        /// <summary>
        /// Gets a delegate that tries to convert the intercepted input parameters to an instance of type <typeparamref name="T"/>.
        /// </summary>
        private Func<(Type type, object? value)[], (bool, T)> TryRecord { get; }

        /// <summary>
        /// Gets the signature of the intercepted method or property setter that is the target for this arrangement.
        /// </summary>
        private MethodInfo Signature { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="IArrangement" />
        public void ApplyTo(IInvocation invocation)
        {
            TryApplyTo(invocation);
        }

        /// <inheritdoc cref="IArrangement" />
        public bool CanApplyTo(IInvocation invocation)
        {
            if (invocation == null)
            {
                return false;
            }

            if (invocation.HasFeature<IParameterIn>())
            {
                return invocation.Signature == Signature;
            }

            return false;
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"Calls to '{Signature.Name}' should record parameter values";
        }

        /// <inheritdoc cref="IArrangement" />
        public bool TryApplyTo(IInvocation invocation)
        {
            if (invocation == null)
            {
                return false;
            }

            if (invocation.TryGetFeature<IParameterIn>(out var parameterInvocation))
            {
                if (invocation.Signature == Signature)
                {
                    var @params = parameterInvocation.InputParameterCollection.Select(p => (p.Type, p.Value)).ToArray();
                    var (canBeRecorded, recordedCall) = TryRecord(@params);
                    if (canBeRecorded)
                    {
                        RecordedCalls.Enqueue(recordedCall);
                    }
                }
            }

            return false;
        }

        #endregion
    }
}