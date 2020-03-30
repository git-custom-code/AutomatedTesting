namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooValueTypeIndexedProperty{T}"/> interface.
    /// </summary>
    public sealed class FooValueTypeIndexedProperty<T> : IFooValueTypeIndexedProperty<T>
        where T : struct
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooValueTypeIndexedProperty{T}"/> type.
        /// </summary>
        /// <param name="returnValue"> The indexed property's getter return value. </param>
        public FooValueTypeIndexedProperty(T returnValue)
        {
            ReturnValue = returnValue;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the indexed property's return value.
        /// </summary>
        private T ReturnValue { get; }

        /// <summary>
        /// Gets the indexed property's set value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the number of times the indexed property's getter was called.
        /// </summary>
        public uint GetterCallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the indexed property's getter parameter.
        /// </summary>
        public T GetterParameter { get; private set; }

        /// <summary>
        /// Gets the number of times the indexed property's setter was called.
        /// </summary>
        public uint SetterCallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the indexed property's setter parameter.
        /// </summary>
        public T SetterParameter { get; private set; }

        #endregion

        #region Data

        /// <inheritdoc />
        public T this[T first]
        {
            get
            {
                GetterCallCount++;
                GetterParameter = first;
                return ReturnValue;
            }
            set
            {
                SetterCallCount++;
                SetterParameter = first;
                Value = value;
            }
        }

        #endregion
    }
}