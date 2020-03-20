namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns an <see cref="IAsyncEnumerable{T}"/> with value types.
    /// </summary>
    public interface IFooAsyncEnumerableValueTypeParameterless : IFoo
    {
        IAsyncEnumerable<int> MethodWithoutParameterAsync();
    }
}