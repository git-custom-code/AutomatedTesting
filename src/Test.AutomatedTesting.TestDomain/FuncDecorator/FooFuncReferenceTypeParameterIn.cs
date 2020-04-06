namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// Test domain implementation of the <see cref="IFooFuncReferenceTypeParameterIn{T}"/> interface.
    /// </summary>
    public sealed class FooFuncReferenceTypeParameterIn<T> : IFooFuncReferenceTypeParameterIn<T>
        where T : class
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FooFuncReferenceTypeParameterIn{T}"/> type.
        /// </summary>
        /// <param name="value"> The method's result value. </param>
        public FooFuncReferenceTypeParameterIn(T? value)
        {
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the number of times the <see cref="MethodWithOneParameter(T?)"/> was called.
        /// </summary>
        public uint CallCount { get; private set; } = 0;

        /// <summary>
        /// Gets the passed parameter values.
        /// </summary>
        public IList<T?> Parameters { get; } = new List<T?>();

        /// <summary>
        /// Gets the method's result value.
        /// </summary>
        private T? Value { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public T? MethodWithOneParameter(T? first)
        {
            CallCount++;
            Parameters.Add(first);
            return Value;
        }

        #endregion
    }
}