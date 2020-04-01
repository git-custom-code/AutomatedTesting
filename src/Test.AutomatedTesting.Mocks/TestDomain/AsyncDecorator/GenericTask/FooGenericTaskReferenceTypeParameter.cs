namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericTaskReferenceTypeParameter"/> interface.
    /// </summary>
    public sealed class FooGenericTaskReferenceTypeParameter : IFooGenericTaskReferenceTypeParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericTaskReferenceTypeParameter"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericTaskReferenceTypeParameter(object? value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameterAsync(object?)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the method's parameter.
        /// </summary>
        public object? Parameter { get; private set; }

        /// <summary>
        /// Gets the method's return value.
        /// </summary>
        private object? Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public Task<object?> MethodWithOneParameterAsync(object? first)
        {
            CallCount++;
            Parameter = first;
            return Task.FromResult(Value);
        }

        #endregion
    }
}