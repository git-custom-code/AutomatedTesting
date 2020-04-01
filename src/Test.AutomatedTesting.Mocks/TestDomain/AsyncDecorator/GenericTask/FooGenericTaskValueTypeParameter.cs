namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericTaskValueTypeParameter"/> interface.
    /// </summary>
    public sealed class FooGenericTaskValueTypeParameter : IFooGenericTaskValueTypeParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericTaskValueTypeParameter"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericTaskValueTypeParameter(int value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameterAsync(int)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the method's parameter.
        /// </summary>
        public int Parameter { get; private set; }

        /// <summary>
        /// Gets the method's return value.
        /// </summary>
        private int Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public Task<int> MethodWithOneParameterAsync(int first)
        {
            CallCount++;
            Parameter = first;
            return Task.FromResult(Value);
        }

        #endregion
    }
}