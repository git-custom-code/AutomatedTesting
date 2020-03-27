namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooActionReferenceTypeParameterOut{T}"/> interface.
    /// </summary>
    /// <typeparam name="T"> The value type of the method's parameters. </typeparam>
    public sealed class FooActionReferenceTypeParameterOut<T> : IFooActionReferenceTypeParameterOut<T>
        where T : class
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooActionReferenceTypeParameterOut{T}"/> type.
        /// </summary>
        /// <param name="value"> The replaced value of the method's out parameter. </param>
        public FooActionReferenceTypeParameterOut(T? value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameter(out T?)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the replaced value of the method's out parameter.
        /// </summary>
        private T? Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void MethodWithOneParameter(out T? first)
        {
            CallCount++;
            first = Value;
        }

        #endregion
    }
}