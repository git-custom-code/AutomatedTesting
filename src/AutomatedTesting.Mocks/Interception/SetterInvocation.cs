namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked property setters.
    /// </summary>
    public sealed class SetterInvocation : IInvocation, IHasInputParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="SetterInvocation"/> type.
        /// </summary>
        /// <param name="signature"> The signature of the invoked property setter (as <see cref="PropertyInfo"/>). </param>
        /// <param name="value"> The property's value. </param>
        public SetterInvocation(PropertyInfo signature, object? value)
        {
            InputParameter = new (Type type, object? value)[] { (signature.PropertyType, value) };
            PropertySignature = signature;
            Signature = signature.GetSetMethod() ?? throw new ArgumentException($"Property {signature.Name} has no setter", nameof(signature));
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public IEnumerable<(Type type, object? value)> InputParameter { get; }

        /// <summary>
        /// Gets the signature of the invoked property (as <see cref="PropertyInfo"/>).
        /// </summary>
        public PropertyInfo PropertySignature { get; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        /// <summary>
        /// Gets the property's value.
        /// </summary>
        public object? Value
        {
            get { return InputParameter.FirstOrDefault().value; }
        }

        #endregion
    }
}