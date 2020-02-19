namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked property getters.
    /// </summary>
    public sealed class GetterInvocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="GetterInvocation"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the invoked property getter (as <see cref="PropertyInfo"/>). </param>
        public GetterInvocation(PropertyInfo signature)
        {
            Signature = signature;
            ReturnValue = null;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets or sets the return value of the intercepted property getter.
        /// </summary>
        public object? ReturnValue { get; set; }

        /// <summary>
        /// Gets the signature of the invoked property getter (as <see cref="PropertyInfo"/>).
        /// </summary>
        public PropertyInfo Signature { get; }

        /// <inheritdoc />
        MemberInfo IInvocation.Signature
        {
            get { return Signature; }
        }

        #endregion
    }
}