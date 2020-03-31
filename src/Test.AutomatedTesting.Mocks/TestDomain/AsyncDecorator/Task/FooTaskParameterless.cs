namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooTaskParameterless"/> interface.
    /// </summary>
    public sealed class FooTaskParameterless : IFooTaskParameterless
    {
        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithoutParameterAsync"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        #endregion

        #region Logic

        /// <inheritdoc />
        public Task MethodWithoutParameterAsync()
        {
            CallCount++;
            return Task.CompletedTask;
        }

        #endregion
    }
}