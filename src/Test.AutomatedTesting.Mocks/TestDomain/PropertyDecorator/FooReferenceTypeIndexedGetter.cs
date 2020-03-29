namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooReferenceTypeIndexedGetter{T}"/> interface.
    /// </summary>
    public sealed class FooReferenceTypeIndexedGetter<T> : IFooReferenceTypeIndexedGetter<T>
        where T : class
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooReferenceTypeIndexedGetter{T}"/> type.
        /// </summary>
        /// <param name="value"> The indexed getter's return value. </param>
        public FooReferenceTypeIndexedGetter(T? value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the indexed getter's value.
        /// </summary>
        private T? Value { get; }

        /// <summary>
        /// Gets the indexed getter's parameter.
        /// </summary>
        public T? Parameter { get; private set; }

        /// <summary>
        /// Gets the number of times the indexed getter was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        #endregion

        #region Data

        /// <inheritdoc />
        public T? this[T? first]
        {
            get
            {
                CallCount++;
                Parameter = first;
                return Value;
            }
        }

        #endregion
    }
}