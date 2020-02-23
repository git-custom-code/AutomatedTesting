namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Reflection;

    /// <summary>
    /// Interface for types that contain information of a method or property invocation
    /// (e.g. the name of the invoked method, the passed parameter values, ...).
    /// </summary>
    public interface IInvocation
    {
        /// <summary>
        /// Gets the signature of the invoked method or property.
        /// </summary>
        MethodInfo Signature { get; }
    }
}