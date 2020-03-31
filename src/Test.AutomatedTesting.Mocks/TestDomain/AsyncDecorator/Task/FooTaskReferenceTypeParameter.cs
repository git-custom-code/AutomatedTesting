namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooTaskReferenceTypeParameter"/> interface.
    /// </summary>
    public sealed class FooTaskReferenceTypeParameter : IFooTaskReferenceTypeParameter
    {
        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameterAsync(object?)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the called method's input parameter.
        /// </summary>
        public object? Parameter { get; private set; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public Task MethodWithOneParameterAsync(object? first)
        {
            CallCount++;
            Parameter = first;
            return Task.CompletedTask;
        }

        #endregion
    }
}