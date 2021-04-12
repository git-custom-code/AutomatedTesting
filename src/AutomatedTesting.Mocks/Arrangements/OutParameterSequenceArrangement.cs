namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using ExceptionHandling;
    using Interception;
    using Interception.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Arrange a sequence of custom out parameter values for an intercepted method call.
    /// </summary>
    /// <typeparam name="T"> The type of the arranged out parameter value sequence. </typeparam>
    public sealed class OutParameterSequenceArrangement<T> : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="OutParameterSequenceArrangement{T}"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method that is the target for this arrangement.
        /// </param>
        /// <param name="outParameterName"> The name of the out parameter. </param>
        /// <param name="outParameterValueSequence"> The arranged sequence of out parameter values. </param>
        public OutParameterSequenceArrangement(MethodInfo signature, string outParameterName, IList<T> outParameterValueSequence)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            OutParameterName = outParameterName ?? throw new ArgumentNullException(nameof(outParameterName));
            OutParameterValueSequence = outParameterValueSequence ?? new List<T>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the name of the out parameter.
        /// </summary>
        private string OutParameterName { get; }

        /// <summary>
        /// Gets the arranged sequence of out parameter values.
        /// </summary>
        private IList<T> OutParameterValueSequence { get; }

        /// <summary>
        /// Gets the <see cref="OutParameterValueSequence"/> index for the next value to be returned.
        /// </summary>
        private int SequenceIndex { get; set; } = 0;

        /// <summary>
        /// Gets the signature of the intercepted method that is the target for this arrangement.
        /// </summary>
        private MethodInfo Signature { get; }

        /// <summary>
        /// Gets a light-weight synchronization object for thread-safety.
        /// </summary>
        private object SyncLock { get; } = new object();

        #endregion

        #region Logic

        /// <inheritdoc cref="IArrangement" />
        public void ApplyTo(IInvocation invocation)
        {
            Ensures.NotNull(invocation, nameof(invocation));

            TryApplyTo(invocation);
        }

        /// <inheritdoc cref="IArrangement" />
        public bool CanApplyTo(IInvocation invocation)
        {
            Ensures.NotNull(invocation, nameof(invocation));

            if (invocation.TryGetFeature<IParameterOut>(out var outParameterFeature))
            {
                if (invocation.Signature == Signature)
                {
                    return outParameterFeature
                        .OutParameterCollection
                        .Any(p => p.Name == OutParameterName && p.Type == typeof(T));
                }
            }

            return false;
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"Calls to '{Signature.Name}' should return '{OutParameterValueSequence[SequenceIndex]}' for out parameter '{OutParameterName}'";
        }

        /// <inheritdoc cref="IArrangement" />
        public bool TryApplyTo(IInvocation invocation)
        {
            Ensures.NotNull(invocation, nameof(invocation));

            if (invocation.Signature == Signature)
            {
                if (invocation.TryGetFeature<IParameterOut>(out var outParameterFeature))
                {
                    var parameter = outParameterFeature
                        .OutParameterCollection
                        .SingleOrDefault(p => p.Name == OutParameterName && p.Type == typeof(T));
                    if (parameter != null)
                    {
                        parameter.Value = GetNextReturnValue();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the next parameter value from the <see cref="OutParameterValueSequence"/> in a thread-safe way.
        /// </summary>
        /// <returns> The next parameter value. </returns>
        private T GetNextReturnValue()
        {
            var parameterValue = OutParameterValueSequence[SequenceIndex];
            if (SequenceIndex < OutParameterValueSequence.Count - 1)
            {
                lock (SyncLock)
                {
                    if (SequenceIndex < OutParameterValueSequence.Count - 1)
                    {
                        SequenceIndex++;
                    }
                }
            }
            return parameterValue;
        }

        #endregion
    }
}