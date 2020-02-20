namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked property setters.
    /// </summary>
    public sealed class SetterInvocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="SetterInvocation"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the invoked property setter (as <see cref="PropertyInfo"/>). </param>
        /// <param name="value"> The property's value. </param>
        public SetterInvocation(PropertyInfo signature, object? value)
        {
            Signature = signature;
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the signature of the invoked property setter (as <see cref="PropertyInfo"/>).
        /// </summary>
        public PropertyInfo Signature { get; }

        /// <inheritdoc />
        MemberInfo IInvocation.Signature
        {
            get { return Signature; }
        }

        /// <summary>
        /// Gets the property's value.
        /// </summary>
        public object? Value { get; }

        #endregion
    }
}