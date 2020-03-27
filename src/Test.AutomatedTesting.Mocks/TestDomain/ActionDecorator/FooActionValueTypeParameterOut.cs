namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooActionValueTypeParameterOut{T}"/> interface.
    /// </summary>
    /// <typeparam name="T"> The value type of the method's parameters. </typeparam>
    public sealed class FooActionValueTypeParameterOut<T> : IFooActionValueTypeParameterOut<T>
        where T : struct
    {
        public FooActionValueTypeParameterOut(T value)
        {
            Value = value;
        }

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameter(out T)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        private T Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void MethodWithOneParameter(out T first)
        {
            CallCount++;
            first = Value;
        }

        #endregion
    }
}