namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericTaskReferenceTypeParameterless"/> interface.
    /// </summary>
    public sealed class FooGenericTaskReferenceTypeParameterless : IFooGenericTaskReferenceTypeParameterless
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericTaskReferenceTypeParameterless"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericTaskReferenceTypeParameterless(object? value)
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
        private object? Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public Task<object?> MethodWithoutParameterAsync()
        {
            CallCount++;
            return Task.FromResult(Value);
        }

        #endregion
    }
}