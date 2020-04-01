namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericValueTaskReferenceTypeParameterless"/> interface.
    /// </summary>
    public sealed class FooGenericValueTaskReferenceTypeParameterless : IFooGenericValueTaskReferenceTypeParameterless
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericValueTaskReferenceTypeParameterless"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericValueTaskReferenceTypeParameterless(object? value)
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
        public ValueTask<object?> MethodWithoutParameterAsync()
        {
            CallCount++;
            return new ValueTask<object?>(Value);
        }

        #endregion
    }
}