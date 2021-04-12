namespace CustomCode.AutomatedTesting.Mocks
{
    using Arrangements;
    using ExceptionHandling;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Default implementation of the <see cref="ICallBehavior{TMock}"/> interface.
    /// </summary>
    /// <typeparam name="TMock"> The type of the interface that is mocked. </typeparam>
    public sealed class CallBehavior<TMock> : ICallBehavior<TMock>
        where TMock : class
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CallBehavior{TMock}"/> type.
        /// </summary>
        /// <param name="arrangements"> The collection of arrangements that have been made for the associated mock. </param>
        /// <param name="signature"> The mocked method or property setter call. </param>
        /// <param name="mockBehavior"> The parent <see cref="IMockBehavior{TMock}"/> that created this instance. </param>
        public CallBehavior(IArrangementCollection arrangements, MethodInfo signature, IMockBehavior<TMock> mockBehavior)
        {
            Arrangements = arrangements ?? throw new ArgumentNullException(nameof(arrangements));
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            MockBehavior = mockBehavior ?? throw new ArgumentNullException(nameof(mockBehavior));
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the collection of arrangements that have been made for the associated mock.
        /// </summary>
        private IArrangementCollection Arrangements { get; }

        /// <summary>
        /// Gets the mocked method or property setter call.
        /// </summary>
        private MethodInfo Signature { get; }

        /// <summary>
        /// Gets the parent <see cref="IMockBehavior{TMock}"/> that created this instance.
        /// </summary>
        private IMockBehavior<TMock> MockBehavior { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1>(out IEnumerable<T1> recordedCalls)
        {
            static (bool, T1) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 1 &&
                    parameter[0].type == typeof(T1))
                {
#nullable disable
                    var value = (T1)parameter[0].value;
                    return (true, value);
                }

                return (false, default);
#nullable restore
            }

            var queue = new ConcurrentQueue<T1>();
            Arrangements.Add(new RecordParameterArrangement<T1>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2>(out IEnumerable<(T1, T2)> recordedCalls)
        {
            static (bool, (T1, T2)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 2 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3>(out IEnumerable<(T1, T2, T3)> recordedCalls)
        {
            static (bool, (T1, T2, T3)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 3 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[3].type == typeof(T3))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4>(out IEnumerable<(T1, T2, T3, T4)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 4 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4, T5>(out IEnumerable<(T1, T2, T3, T4, T5)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4, T5)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 5 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4) &&
                    parameter[4].type == typeof(T5))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value,
                            (T5)parameter[4].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4, T5)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4, T5)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6>(out IEnumerable<(T1, T2, T3, T4, T5, T6)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4, T5, T6)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 6 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4) &&
                    parameter[4].type == typeof(T5) &&
                    parameter[5].type == typeof(T6))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value,
                            (T5)parameter[4].value,
                            (T6)parameter[5].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4, T5, T6)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4, T5, T6)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4, T5, T6, T7)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 7 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4) &&
                    parameter[4].type == typeof(T5) &&
                    parameter[5].type == typeof(T6) &&
                    parameter[6].type == typeof(T7))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value,
                            (T5)parameter[4].value,
                            (T6)parameter[5].value,
                            (T7)parameter[6].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4, T5, T6, T7)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4, T5, T6, T7)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7, T8>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4, T5, T6, T7, T8)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 8 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4) &&
                    parameter[4].type == typeof(T5) &&
                    parameter[5].type == typeof(T6) &&
                    parameter[6].type == typeof(T7) &&
                    parameter[7].type == typeof(T8))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value,
                            (T5)parameter[4].value,
                            (T6)parameter[5].value,
                            (T7)parameter[6].value,
                            (T8)parameter[7].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4, T5, T6, T7, T8)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4, T5, T6, T7, T8)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Records<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> recordedCalls)
        {
            static (bool, (T1, T2, T3, T4, T5, T6, T7, T8, T9)) TryRecord((Type type, object? value)[] parameter)
            {
                if (parameter.Length == 9 &&
                    parameter[0].type == typeof(T1) &&
                    parameter[1].type == typeof(T2) &&
                    parameter[2].type == typeof(T3) &&
                    parameter[3].type == typeof(T4) &&
                    parameter[4].type == typeof(T5) &&
                    parameter[5].type == typeof(T6) &&
                    parameter[6].type == typeof(T7) &&
                    parameter[7].type == typeof(T8) &&
                    parameter[8].type == typeof(T9))
                {
                    var values =
                        (
#nullable disable
                            (T1)parameter[0].value,
                            (T2)parameter[1].value,
                            (T3)parameter[2].value,
                            (T4)parameter[3].value,
                            (T5)parameter[4].value,
                            (T6)parameter[5].value,
                            (T7)parameter[6].value,
                            (T8)parameter[7].value,
                            (T9)parameter[8].value
                        );
                    return (true, values);
#nullable restore
                }

                return (false, default);
            }

            var queue = new ConcurrentQueue<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>();
            Arrangements.Add(new RecordParameterArrangement<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(Signature, queue, TryRecord));
            recordedCalls = queue;
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> ReturnsOutParameterValue<T>(string outParameterName, T outParameterValue)
        {
            var arrangement = new OutParameterArrangement<T>(Signature, outParameterName, outParameterValue);
            Arrangements.Add(arrangement);
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> ReturnsOutParameterSequence<T>(string outParameterName, params T[] outParameterSequence)
        {
            Ensures.NotNull(outParameterSequence, nameof(outParameterSequence));

            var sequence = new List<T>(outParameterSequence);
            var arrangement = new OutParameterSequenceArrangement<T>(Signature, outParameterName, sequence);
            Arrangements.Add(arrangement);
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> ReturnsRefParameterValue<T>(string refParameterName, T refParameterValue)
        {
            var arrangement = new RefParameterArrangement<T>(Signature, refParameterName, refParameterValue);
            Arrangements.Add(arrangement);
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> ReturnsRefParameterSequence<T>(string refParameterName, params T[] refParameterSequence)
        {
            Ensures.NotNull(refParameterSequence, nameof(refParameterSequence));

            var sequence = new List<T>(refParameterSequence);
            var arrangement = new RefParameterSequenceArrangement<T>(Signature, refParameterName, sequence);
            Arrangements.Add(arrangement);
            return this;
        }

        /// <inheritdoc cref="IMockBehavior{TMock}"/>
        public ICallBehavior<TMock> That(Expression<Action<TMock>> mockedCall)
        {
            return MockBehavior.That(mockedCall);
        }

        /// <inheritdoc cref="IMockBehavior{TMock}"/>
        public ICallBehavior<TMock, TResult> That<TResult>(Expression<Func<TMock, TResult>> mockedCall)
        {
            return MockBehavior.That(mockedCall);
        }

        /// <inheritdoc cref="IMockBehavior{TMock}"/>
        public ICallBehavior<TMock> ThatAssigning(Action<TMock> mockedSetterCall)
        {
            return MockBehavior.ThatAssigning(mockedSetterCall);
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Throws<T>() where T : Exception, new()
        {
            var arrangement = new ExceptionArrangement(Signature, () => new T());
            Arrangements.Add(arrangement);
            return this;
        }

        /// <inheritdoc cref="ICallBehavior{TMock}" />
        public ICallBehavior<TMock> Throws(Exception exception)
        {
            Ensures.NotNull(exception, nameof(exception));

            var arrangement = new ExceptionArrangement(Signature, () => exception);
            Arrangements.Add(arrangement);
            return this;
        }

        #endregion
    }
}