namespace CustomCode.AutomatedTesting.Mocks.Interception.Properties
{
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of properties
    /// </summary>
    public sealed class PropertyInvocation : IPropertyInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyInfo"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the invoked property. </param>
        public PropertyInvocation(PropertyInfo signature)
        {
            Signature = signature;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public PropertyInfo Signature { get; }

        #endregion
    }
}