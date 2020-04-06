namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooFuncValueTypeParameterless{T}"/> interface.
    /// </summary>
    public sealed class FooFuncValueTypeParameterless<T> : IFooFuncValueTypeParameterless<T>
        where T : struct
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooFuncValueTypeParameterless{T}"/> type.
        /// </summary>
        /// <param name="value"> The method's result value. </param>
        public FooFuncValueTypeParameterless(T value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithoutParameter"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the method's result value.
        /// </summary>
        private T Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public T MethodWithoutParameter()
        {
            CallCount++;
            return Value;
        }

        #endregion
    }
}