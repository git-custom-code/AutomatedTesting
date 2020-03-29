namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooReferenceTypeIndexedSetter{T}"/> interface.
    /// </summary>
    public sealed class FooReferenceTypeIndexedSetter<T> : IFooReferenceTypeIndexedSetter<T>
        where T : class
    {
        #region Data

        /// <summary>
        /// Gets the indexed setter's value.
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        /// Gets the indexed setter's parameter.
        /// </summary>
        public T? Parameter { get; private set; }

        /// <summary>
        /// Gets the number of times the indexed setter was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        #endregion

        #region Data

        /// <inheritdoc />
        public T? this[T? first]
        {
            set
            {
                CallCount++;
                Parameter = first;
                Value = value;
            }
        }

        #endregion
    }
}