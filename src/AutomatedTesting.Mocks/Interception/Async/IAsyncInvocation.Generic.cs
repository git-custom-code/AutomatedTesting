namespace CustomCode.AutomatedTesting.Mocks.Interception.Async;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Generic feature interface for an <see cref="IInvocation"/> of an asynchronous method.
/// </summary>
/// <typeparam name="T">
/// The type of the asynchronous return value (<see cref="Task"/>, <see cref="Task{TResult}"/>,
/// <see cref="ValueTask"/>, <see cref="ValueTask{TResult}"/> or <see cref="IAsyncEnumerable{TResult}"/>).
/// </typeparam>
public interface IAsyncInvocation<T> : IAsyncInvocation
{
    /// <summary>
    /// Gets or sets the asynchronous return value of the intercepted method.
    /// </summary>
    T AsyncReturnValue { get; set; }
}
