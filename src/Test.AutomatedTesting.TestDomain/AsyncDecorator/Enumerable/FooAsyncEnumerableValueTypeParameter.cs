namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooAsyncEnumerableValueTypeParameter"/> interface.
    /// </summary>
    public sealed class FooAsyncEnumerableValueTypeParameter : IFooAsyncEnumerableValueTypeParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooAsyncEnumerableValueTypeParameter"/> type.
        /// </summary>
        /// <param name="result"> The method's return value. </param>
        public FooAsyncEnumerableValueTypeParameter(IEnumerable<int> result)
        {
            Result = result.ToAsyncEnumerable();
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
        private IAsyncEnumerable<int> Result { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public IAsyncEnumerable<int> MethodWithOneParameterAsync(int first)
        {
            CallCount++;
            Parameter = first;
            return Result;
        }

        #endregion
    }
}