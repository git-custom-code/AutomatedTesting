namespace CustomCode.AutomatedTesting.Mocks.Interception.Async
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
    /// that return a <see cref="ValueTask"/>.
    /// </summary>
    public sealed class AsyncValueTaskInvocation : IAsyncInvocation<ValueTask>
    {
        #region Data

        /// <inheritdoc />
        public ValueTask AsyncReturnValue { get; set; } = default;

        /// <inheritdoc />
        public AsyncInvocationType Type { get; } = AsyncInvocationType.ValueTask;

        #endregion
    }
}