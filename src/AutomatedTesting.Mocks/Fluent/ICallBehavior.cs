namespace CustomCode.AutomatedTesting.Mocks;

using Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

/// <summary>
/// Fluent API interface that is returned by <see cref="IMockBehavior{TMock}.That(Expression{System.Action{TMock}})"/>
/// calls and can be used to setup the behavior of mocked method or property calls with <see cref="void"/> return type.
/// </summary>
/// <typeparam name="TMock"> The type of the interface that is mocked. </typeparam>
public interface ICallBehavior<TMock> : IMockBehavior<TMock>, IFluentInterface
    where TMock : class
{
    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1>(out IEnumerable<T1> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2>(out IEnumerable<(T1, T2)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3>(out IEnumerable<(T1, T2, T3)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4>(out IEnumerable<(T1, T2, T3, T4)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <typeparam name="T5"> The type of the fifth input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4, T5>(out IEnumerable<(T1, T2, T3, T4, T5)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <typeparam name="T5"> The type of the fifth input parameter. </typeparam>
    /// <typeparam name="T6"> The type of the sixth input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6>(out IEnumerable<(T1, T2, T3, T4, T5, T6)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <typeparam name="T5"> The type of the fifth input parameter. </typeparam>
    /// <typeparam name="T6"> The type of the sixth input parameter. </typeparam>
    /// <typeparam name="T7"> The type of the seventh input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <typeparam name="T5"> The type of the fifth input parameter. </typeparam>
    /// <typeparam name="T6"> The type of the sixth input parameter. </typeparam>
    /// <typeparam name="T7"> The type of the seventh input parameter. </typeparam>
    /// <typeparam name="T8"> The type of the eigth input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7, T8>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> recordedCalls);

    /// <summary>
    /// Record the (input) parameters of each mocked method or property setter call.
    /// </summary>
    /// <typeparam name="T1"> The type of the first input parameter. </typeparam>
    /// <typeparam name="T2"> The type of the second input parameter. </typeparam>
    /// <typeparam name="T3"> The type of the third input parameter. </typeparam>
    /// <typeparam name="T4"> The type of the fourth input parameter. </typeparam>
    /// <typeparam name="T5"> The type of the fifth input parameter. </typeparam>
    /// <typeparam name="T6"> The type of the sixth input parameter. </typeparam>
    /// <typeparam name="T7"> The type of the seventh input parameter. </typeparam>
    /// <typeparam name="T8"> The type of the eigth input parameter. </typeparam>
    /// <typeparam name="T9"> The type of the ninth input parameter. </typeparam>
    /// <param name="recordedCalls"> A collection with the recorded input parameters. </param>
    ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> recordedCalls);

    /// <summary>
    /// Setup the given <paramref name="outParameterValue"/> for the out parameter with the name
    /// <paramref name="outParameterName"/> after the mocked method is called.
    /// </summary>
    /// <typeparam name="T"> The type of the mocked out parameter. </typeparam>
    /// <param name="outParameterName"> The name of the mocked out parameter. </param>
    /// <param name="outParameterValue"> The value of the mocked out parameter after the method is called. </param>
    ICallBehavior<TMock> ReturnsOutParameterValue<T>(string outParameterName, T outParameterValue);

    /// <summary>
    /// Setup a sequence of constant values. Each subsequent mocked method or property call will return
    /// the next value in the specified <paramref name="outParameterSequence"/>.
    /// </summary>
    /// <param name="outParameterName"> The name of the mocked out parameter. </param>
    /// <param name="outParameterSequence"> A sequence of constant values for the mocked out parameter. </param>
    /// <remarks>
    /// Additional calls will always return the last value of the sequence.
    /// </remarks>
    ICallBehavior<TMock> ReturnsOutParameterSequence<T>(string outParameterName, params T[] outParameterSequence);

    /// <summary>
    /// Setup the given <paramref name="refParameterValue"/> for the ref parameter with the name
    /// <paramref name="refParameterName"/> after the mocked method is called.
    /// </summary>
    /// <typeparam name="T"> The type of the mocked ref parameter. </typeparam>
    /// <param name="refParameterName"> The name of the mocked ref parameter. </param>
    /// <param name="refParameterValue"> The value of the mocked ref parameter after the method is called. </param>
    ICallBehavior<TMock> ReturnsRefParameterValue<T>(string refParameterName, T refParameterValue);

    /// <summary>
    /// Setup a sequence of constant values. Each subsequent mocked method or property call will return
    /// the next value in the specified <paramref name="refParameterSequence"/>.
    /// </summary>
    /// <param name="refParameterName"> The name of the mocked ref parameter. </param>
    /// <param name="refParameterSequence"> A sequence of constant values for the mocked ref parameter. </param>
    /// <remarks>
    /// Additional calls will always return the last value of the sequence.
    /// </remarks>
    ICallBehavior<TMock> ReturnsRefParameterSequence<T>(string refParameterName, params T[] refParameterSequence);

    /// <summary>
    /// Setup an exception that should be thrown every time the mocked method or property setter is called.
    /// </summary>
    /// <typeparam name="T"> The type of the exception to be thrown. </typeparam>
    ICallBehavior<TMock> Throws<T>() where T : Exception, new();

    /// <summary>
    /// Setup an exception that should be thrown every time the mocked method or property setter is called.
    /// </summary>
    /// <param name="exception"> The exception that should be thrown. </param>
    ICallBehavior<TMock> Throws(Exception exception);
}
