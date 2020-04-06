namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooAsyncEnumerableReferenceTypeParameterless"/> interface.
    /// </summary>
    public sealed class FooAsyncEnumerableReferenceTypeParameterless : IFooAsyncEnumerableReferenceTypeParameterless
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooAsyncEnumerableReferenceTypeParameterless"/> type.
        /// </summary>
        /// <param name="result"> The method's return value. </param>
        public FooAsyncEnumerableReferenceTypeParameterless(IEnumerable<object?> result)
        {
            Result = result.ToAsyncEnumerable();
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
        private IAsyncEnumerable<object?> Result { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public IAsyncEnumerable<object?> MethodWithoutParameterAsync()
        {
            CallCount++;
            return Result;
        }

        #endregion
    }
}