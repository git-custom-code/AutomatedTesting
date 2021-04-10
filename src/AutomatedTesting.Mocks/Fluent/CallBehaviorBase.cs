namespace CustomCode.AutomatedTesting.Mocks
{
    using Arrangements;
    using ExceptionHandling;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Base type that for the <see cref="CallBehavior"/> and <see cref="CallBehavior{TResult}"/>
    /// specializations.
    /// </summary>
    public abstract class CallBehaviorBase : ICallBehaviorBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CallBehaviorBase"/> type.
        /// </summary>
        /// <param name="arrangements"> The collection of arrangements that have been made for the associated mock. </param>
        /// <param name="signature"> The mocked method or property setter call. </param>
        protected CallBehaviorBase(IArrangementCollection arrangements, MethodInfo signature)
        {
            Arrangements = arrangements ?? throw new ArgumentNullException(nameof(arrangements));
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the collection of arrangements that have been made for the associated mock.
        /// </summary>
        protected IArrangementCollection Arrangements { get; }

        /// <summary>
        /// Gets the mocked method or property setter call.
        /// </summary>
        protected MethodInfo Signature { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1>(out IEnumerable<T1> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2>(out IEnumerable<(T1, T2)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3>(out IEnumerable<(T1, T2, T3)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4>(out IEnumerable<(T1, T2, T3, T4)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4, T5>(out IEnumerable<(T1, T2, T3, T4, T5)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4, T5, T6>(out IEnumerable<(T1, T2, T3, T4, T5, T6)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4, T5, T6, T7>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4, T5, T6, T7, T8>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Records<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> recordedCalls)
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
        }

        /// <inheritdoc cref="ICallBehavior{TResult}" />
        public void ReturnsOutParameterValue<T>(string outParameterName, T outParameterValue)
        {
            var arrangement = new OutParameterArrangement<T>(Signature, outParameterName, outParameterValue);
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Throws<T>() where T : Exception, new()
        {
            var arrangement = new ExceptionArrangement(Signature, () => new T());
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc cref="ICallBehavior" />
        public void Throws(Exception exception)
        {
            Ensures.NotNull(exception, nameof(exception));

            var arrangement = new ExceptionArrangement(Signature, () => exception);
            Arrangements.Add(arrangement);
        }

        #endregion
    }
}