namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooFuncReferenceTypeParameterless{T}"/> interface.
    /// </summary>
    public sealed class FooFuncReferenceTypeParameterless<T> : IFooFuncReferenceTypeParameterless<T>
        where T : class
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooFuncReferenceTypeParameterless{T}"/> type.
        /// </summary>
        /// <param name="value"> The method's result value. </param>
        public FooFuncReferenceTypeParameterless(T? value)
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
        private T? Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public T? MethodWithoutParameter()
        {
            CallCount++;
            return Value;
        }

        #endregion
    }
}