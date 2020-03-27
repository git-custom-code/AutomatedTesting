namespace CustomCode.AutomatedTesting.Mocks
{
    using Interception;

    /// <summary>
    /// Enumeration that defines the behavior of a dynamic proxy for a mocked dependency.
    /// </summary>
    public enum MockBehavior : byte
    {
        /// <summary>
        /// If no <see cref="IArrangement"/> was setup, mocked methods or properties will automatically
        /// return the default value (see <see cref="LooseMockInterceptor"/> for more details).
        /// </summary>
        Loose = 0,

        /// <summary>
        /// If no <see cref="IArrangement"/> was setup, mocked methods or properties will automatically
        /// call to the concrete implementation (see <see cref="PartialMockInterceptor"/> for more details).
        /// </summary>
        Partial = 1
    }
}