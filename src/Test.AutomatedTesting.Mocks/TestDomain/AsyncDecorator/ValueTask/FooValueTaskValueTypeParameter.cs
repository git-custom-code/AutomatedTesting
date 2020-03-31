namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooValueTaskValueTypeParameter"/> interface.
    /// </summary>
    public sealed class FooValueTaskValueTypeParameter : IFooValueTaskValueTypeParameter
    {
        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameterAsync(int)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the called method's input parameter.
        /// </summary>
        public int Parameter { get; private set; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public ValueTask MethodWithOneParameterAsync(int first)
        {
            CallCount++;
            Parameter = first;
            return new ValueTask();
        }

        #endregion
    }
}