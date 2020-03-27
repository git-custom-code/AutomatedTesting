namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooFuncValueTypeParameterOut{T}"/> interface.
    /// </summary>
    /// <typeparam name="T"> The value type of the method's parameters. </typeparam>
    public sealed class FooFuncValueTypeParameterOut<T> : IFooFuncValueTypeParameterOut<T>
        where T : struct
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooFuncValueTypeParameterOut{T}"/> type.
        /// </summary>
        /// <param name="returnValue"> The method's result value. </param>
        /// <param name="value"> The replaced value of the method's out parameter. </param>
        public FooFuncValueTypeParameterOut(T returnValue, T value)
        {
            ReturnValue = returnValue;
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameter(out T)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the method's result value.
        /// </summary>
        private T ReturnValue { get; }

        /// <summary>
        /// Gets the replaced value of the method's out parameter.
        /// </summary>
        private T Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public T MethodWithOneParameter(out T first)
        {
            CallCount++;
            first = Value;
            return ReturnValue;
        }

        #endregion
    }
}