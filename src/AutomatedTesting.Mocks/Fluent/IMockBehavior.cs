namespace CustomCode.AutomatedTesting.Mocks;

using Fluent;
using System;
using System.Linq.Expressions;

/// <summary>
/// Fluent API interface that is returned by <see cref="IMocked{T}.ArrangeFor{TMock}"/> calls
/// and can be used to setup the behavior of mocked method or property calls.
/// </summary>
/// <typeparam name="TMock"> The type of the interface that is mocked. </typeparam>
public interface IMockBehavior<TMock> : IFluentInterface
    where TMock : class
{
    /// <summary>
    /// Setup the behavior of a call to a method with <see cref="void"/> return type.
    /// </summary>
    /// <param name="mockedCall"> A lambda that defines the mocked method call. </param>
    /// <returns> An <see cref="ICallBehavior{TMock}"/> instance that can be used to setup the behavior. </returns>
    ICallBehavior<TMock> That(Expression<Action<TMock>> mockedCall);

    /// <summary>
    /// Setup the behavior of a call to a method or property getter that returns a value of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult"> The type of the result value of the mocked method or property getter call. </typeparam>
    /// <param name="mockedCall"> A lambda that defines the mocked method or property getter call. </param>
    /// <returns> An <see cref="ICallBehavior{TMock, TResult}"/> instance that can be used to setup the behavior. </returns>
    ICallBehavior<TMock, TResult> That<TResult>(Expression<Func<TMock, TResult>> mockedCall);

    /// <summary>
    /// Setup the behavior of a call to a property setter (since <see cref="Expression"/> trees
    /// cannot contain an assignment operator).
    /// </summary>
    /// <param name="mockedSetterCall"> A lambda that defines the mocked property setter call. </param>
    /// <returns> An <see cref="ICallBehavior{TMock}"/> instance that can be used to setup the behavior. </returns>
    ICallBehavior<TMock> ThatAssigning(Action<TMock> mockedSetterCall);
}
