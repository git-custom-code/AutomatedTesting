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
    /// Arrange a sequence of custom ref parameter values for an intercepted method call.
    /// </summary>
    /// <typeparam name="T"> The type of the arranged ref parameter value sequence. </typeparam>
    public sealed class RefParameterSequenceArrangement<T> : IArrangement
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="RefParameterSequenceArrangement{T}"/> type.
        /// </summary>
        /// <param name="signature">
        /// The signature of the intercepted method that is the target for this arrangement.
        /// </param>
        /// <param name="refParameterName"> The name of the ref parameter. </param>
        /// <param name="refParameterValueSequence"> The arranged sequence of ref parameter values. </param>
        public RefParameterSequenceArrangement(MethodInfo signature, string refParameterName, IList<T> refParameterValueSequence)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            RefParameterName = refParameterName ?? throw new ArgumentNullException(nameof(refParameterName));
            RefParameterValueSequence = refParameterValueSequence ?? new List<T>();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the name of the ref parameter.
        /// </summary>
        private string RefParameterName { get; }

        /// <summary>
        /// Gets the arranged sequence of ref parameter values.
        /// </summary>
        private IList<T> RefParameterValueSequence { get; }

        /// <summary>
        /// Gets the <see cref="RefParameterValueSequence"/> index for the next value to be returned.
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

            if (invocation.TryGetFeature<IParameterRef>(out var refParameterFeature))
            {
                if (invocation.Signature == Signature)
                {
                    return refParameterFeature
                        .RefParameterCollection
                        .Any(p => p.Name == RefParameterName && p.Type == typeof(T));
                }
            }

            return false;
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"Calls to '{Signature.Name}' should return '{RefParameterValueSequence[SequenceIndex]}' for ref parameter '{RefParameterName}'";
        }

        /// <inheritdoc cref="IArrangement" />
        public bool TryApplyTo(IInvocation invocation)
        {
            Ensures.NotNull(invocation, nameof(invocation));

            if (invocation.Signature == Signature)
            {
                if (invocation.TryGetFeature<IParameterRef>(out var refParameterFeature))
                {
                    var parameter = refParameterFeature
                        .RefParameterCollection
                        .SingleOrDefault(p => p.Name == RefParameterName && p.Type == typeof(T));
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
        /// Gets the next parameter value from the <see cref="RefParameterValueSequence"/> in a thread-safe way.
        /// </summary>
        /// <returns> The next parameter value. </returns>
        private T GetNextReturnValue()
        {
            var parameterValue = RefParameterValueSequence[SequenceIndex];
            if (SequenceIndex < RefParameterValueSequence.Count - 1)
            {
                lock (SyncLock)
                {
                    if (SequenceIndex < RefParameterValueSequence.Count - 1)
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