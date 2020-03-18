namespace CustomCode.AutomatedTesting.Mocks.Interception.Properties
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for intercepted property setters.
    /// </summary>
    public sealed class PropertySetterValue : IPropertySetterValue
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="PropertySetterValue"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the property. </param>
        /// <param name="value"> The value of the property's setter. </param>
        public PropertySetterValue(PropertyInfo signature, object? value)
        {
            Signature = signature;
            Type = signature.PropertyType;
            Value = value;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public PropertyInfo Signature { get; }

        /// <inheritdoc />
        public Type Type { get; }

        /// <inheritdoc />
        public object? Value { get; }

        #endregion
    }
}