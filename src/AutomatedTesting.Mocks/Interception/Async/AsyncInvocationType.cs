namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    /// <summary>
    /// Enumeration that defines the type of an asynchronous <see cref="IInvocation"/>.
    /// </summary>
    public enum AsyncInvocationType
    {
        /// <summary>
        /// The async method invocation will return a <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        Task = 0,

        /// <summary>
        /// The async method invocation will return a <see cref="System.Threading.Tasks.Task{TResult}"/>.
        /// </summary>
        GenericTask = 1,

        /// <summary>
        /// The async method invocation will return a <see cref="System.Threading.Tasks.ValueTask"/>.
        /// </summary>
        ValueTask = 2,

        /// <summary>
        /// The async method invocation will return a <see cref="System.Threading.Tasks.ValueTask{TResult}"/>.
        /// </summary>
        GenericValueTask = 3,

        /// <summary>
        /// The async method invocation will return a <see cref="System.Collections.Generic.IAsyncEnumerable{T}"/>.
        /// </summary>
        AsyncEnumerable = 4
    }
}