namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;
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
            PropertySignature = signature;
            ReturnValue = null;
            Signature = signature.GetGetMethod() ?? throw new ArgumentException($"Property {signature.Name} has no getter", nameof(signature));
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the signature of the invoked property. (as <see cref="PropertyInfo"/>).
        /// </summary>
        public PropertyInfo PropertySignature { get; }

        /// <summary>
        /// Gets or sets the return value of the intercepted property getter.
        /// </summary>
        public object? ReturnValue { get; set; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        #endregion
    }
}