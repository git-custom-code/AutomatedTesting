namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns an <see cref="IAsyncEnumerable{T}"/> with reference types.
    /// </summary>
    public interface IFooAsyncEnumerableReferenceTypeParameterless : IFoo
    {
        IAsyncEnumerable<object?> MethodWithoutParameterAsync();
    }
}