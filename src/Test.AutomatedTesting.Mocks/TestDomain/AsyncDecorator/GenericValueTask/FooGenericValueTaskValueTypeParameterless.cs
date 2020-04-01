namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericValueTaskValueTypeParameterless"/> interface.
    /// </summary>
    public sealed class FooGenericValueTaskValueTypeParameterless : IFooGenericValueTaskValueTypeParameterless
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericValueTaskValueTypeParameterless"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericValueTaskValueTypeParameterless(int value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithoutParameterAsync"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the method's return value.
        /// </summary>
        private int Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public ValueTask<int> MethodWithoutParameterAsync()
        {
            CallCount++;
            return new ValueTask<int>(Value);
        }

        #endregion
    }
}