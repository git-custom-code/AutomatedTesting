namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooGenericValueTaskReferenceTypeParameter"/> interface.
    /// </summary>
    public sealed class FooGenericValueTaskReferenceTypeParameter : IFooGenericValueTaskReferenceTypeParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooGenericValueTaskReferenceTypeParameter"/> type.
        /// </summary>
        /// <param name="value"> The method's return value. </param>
        public FooGenericValueTaskReferenceTypeParameter(object? value)
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
        public ValueTask<object?> MethodWithOneParameterAsync(object? first)
        {
            CallCount++;
            Parameter = first;
            return new ValueTask<object?>(Value);
        }

        #endregion
    }
}