namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using ExceptionHandling;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Default implementation of the <see cref="IInvocation"/> interface.
    /// </summary>
    public sealed class Invocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Invocation"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the invoked method or property. </param>
        /// <param name="features"> A collection of present invocation features. </param>
        public Invocation(MethodInfo signature, params IInvocationFeature[] features)
        {
            Signature = signature;
            Features = new HashSet<IInvocationFeature>(features);
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a collection of present invocation features.
        /// </summary>
        private HashSet<IInvocationFeature> Features { get; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public T GetFeature<T>() where T : class, IInvocationFeature
        {
            var feature = (T?)Features.SingleOrDefault(f => f is T);
            if (feature == null)
            {
                throw new MissingFeatureException(this, typeof(T));
            }
            return feature;
        }

        /// <inheritdoc />
        public bool HasFeature<T>() where T : class, IInvocationFeature
        {
            return Features.Any(feature => feature is T);
        }

        /// <inheritdoc />
        public bool TryGetFeature<T>([NotNullWhen(true)] out T? feature) where T : class, IInvocationFeature
        {
            feature = Features.Where(f => f is T).SingleOrDefault() as T;
            return feature != null;
        }

        #endregion
    }
}