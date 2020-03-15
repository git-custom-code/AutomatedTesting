namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    /// <summary>
    /// Interface for features of a method or property <see cref="IInvocation"/>.
    /// </summary>
    public interface IInvocationFeature
    {
        /// <summary>
        /// Get the feature's hash code.
        /// </summary>
        /// <returns> The computed hash code. </returns>
        int GetHashCode();
    }
}