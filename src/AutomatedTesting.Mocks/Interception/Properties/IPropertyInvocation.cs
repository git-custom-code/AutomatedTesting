namespace CustomCode.AutomatedTesting.Mocks.Interception.Properties
{
    using System.Reflection;

    /// <summary>
    /// Non-generic feature interface for an <see cref="IInvocation"/> of a property.
    /// </summary>
    public interface IPropertyInvocation : IInvocationFeature
    {
        /// <summary>
        /// Gets the signature of the invoked property. 
        /// </summary>
        PropertyInfo Signature { get; } 
    }
}