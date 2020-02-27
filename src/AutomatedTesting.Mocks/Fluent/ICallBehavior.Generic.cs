namespace CustomCode.AutomatedTesting.Mocks.Fluent
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Fluent API interface that is returned by <see cref="IMockBehavior{TMock}.That{TResult}(Expression{Func{TMock, TResult}})"/>
    /// calls and can be used to setup the behavior of mocked method or property calls that return a value.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the result value that is returned by the mocked method or property getter.
    /// </typeparam>
    public interface ICallBehavior<TResult> : IFluentInterface
    {
        /// <summary>
        /// Setup a constant value that should be returned every time the mocked method or property is called.
        /// </summary>
        /// <param name="returnValue"> The value to be returned. </param>
        void Returns(TResult returnValue);

        /// <summary>
        /// Setup a sequence of constant values. Each subsequent mocked method or property call will return
        /// the next value in the specified <paramref name="returnValueSequence"/>.
        /// </summary>
        /// <param name="returnValueSequence"> A sequence of constant values to be returned. </param>
        /// <remarks>
        /// Additional calls will always return the last value of the sequence.
        /// </remarks>
        void ReturnsSequence(params TResult[] returnValueSequence);

        /// <summary>
        /// Setup an exception that should be thrown every time the mocked method or property is called.
        /// </summary>
        /// <typeparam name="T"> The type of the exception to be thrown. </typeparam>
        void Throws<T>() where T : Exception, new();

        /// <summary>
        /// Setup an exception that should be thrown every time the mocked method or property is called.
        /// </summary>
        /// <param name="exception"> The exception that should be thrown. </param>
        void Throws(Exception exception);
    }
}