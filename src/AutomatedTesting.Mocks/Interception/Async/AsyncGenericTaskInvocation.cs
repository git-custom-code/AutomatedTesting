namespace CustomCode.AutomatedTesting.Mocks.Interception.Async;

using Interception.ReturnValue;
using System.Threading.Tasks;

/// <summary>
/// Implementation of an <see cref="IInvocationFeature"/> for invocations of asynchronous methods
/// that return a <see cref="Task{TResult}"/>.
/// </summary>
/// <typeparam name="TResult"> The type of the asynchronous result value. </typeparam>
public sealed class AsyncGenericTaskInvocation<TResult> :
    IAsyncInvocation<Task<TResult>>, IReturnValue<TResult>
{
    #region Data

    /// <inheritdoc cref="IAsyncInvocation{T}" />
#nullable disable
    public Task<TResult> AsyncReturnValue { get; set; } = Task.FromResult<TResult>(default);
#nullable restore

    /// <inheritdoc cref="IReturnValue{T}" />
    TResult IReturnValue<TResult>.ReturnValue
    {
        get { return AsyncReturnValue.ConfigureAwait(false).GetAwaiter().GetResult(); }
#nullable disable
        set { AsyncReturnValue = Task.FromResult(value); }
#nullable restore
    }

    /// <inheritdoc cref="IReturnValue" />
    object? IReturnValue.ReturnValue
    {
        get { return (object?)((IReturnValue<TResult>)this).ReturnValue; }
#nullable disable
        set { ((IReturnValue<TResult>)this).ReturnValue = (TResult)value; }
#nullable restore
    }

    /// <inheritdoc cref="IAsyncInvocation" />
    public AsyncInvocationType Type { get; } = AsyncInvocationType.GenericTask;

    #endregion
}
