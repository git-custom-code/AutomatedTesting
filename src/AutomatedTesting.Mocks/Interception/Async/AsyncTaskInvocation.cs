namespace CustomCode.AutomatedTesting.Mocks.Interception.Async;

using System.Threading.Tasks;

/// <summary>
/// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
/// that return a <see cref="Task"/>.
/// </summary>
public sealed class AsyncTaskInvocation : IAsyncInvocation<Task>
{
    #region Data

    /// <inheritdoc cref="IAsyncInvocation{T}" />
    public Task AsyncReturnValue { get; set; } = Task.CompletedTask;

    /// <inheritdoc cref="IAsyncInvocation" />
    public AsyncInvocationType Type { get; } = AsyncInvocationType.Task;

    #endregion
}
