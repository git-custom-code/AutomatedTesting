namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Test domain implementation of the <see cref="IFooValueTypeGetter{T}"/> interface.
    /// </summary>
    public sealed class FooValueTypeGetter<T> : IFooValueTypeGetter<T>
        where T : struct
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooValueTypeGetter{T}"/> type.
        /// </summary>
        /// <param name="value"> The <see cref="Getter"/>'s return value. </param>
        public FooValueTypeGetter(T value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the <see cref="Getter"/>'s return value.
        /// </summary>
        private T Value { get; }

        /// <summary>
        /// Gets the number of times the <see cref="Getter"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        #endregion

        #region Data

        /// <inheritdoc />
        public T Getter
        {
            get
            {
                CallCount++;
                return Value;
            }
        }

        #endregion
    }
}