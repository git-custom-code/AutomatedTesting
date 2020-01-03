namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    /// <summary>
    /// Interceptor implementations can be injected into dynamically created mock instances in order
    /// to intercept method and/or property calls and executed custom logic instead.
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// Intercept a method or property call.
        /// </summary>
        /// <param name="invocation">
        /// Contains relevant information about the original method/property call like e.g. signatrue, parameters or return value.
        /// </param>
        void Intercept(IInvocation invocation);
    }
}