namespace CustomCode.AutomatedTesting.Mocks.Interception.Properties
{
    using System;

    /// <summary>
    /// Non-generic feature interface for an <see cref="IInvocation"/> of a property setter.
    /// </summary>
    public interface IPropertySetterValue : IInvocationFeature
    {
        /// <summary>
        /// Gets the type of the setter's value.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the setter's value.
        /// </summary>
        object? Value { get; }
    }
}