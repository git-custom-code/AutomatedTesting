namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// with a value type parameter that returns an <see cref="IAsyncEnumerable{T}"/> with value types.
    /// </summary>
    public interface IFooAsyncEnumerableValueTypeParameter : IFoo
    {
        IAsyncEnumerable<int> MethodWithOneParameterAsync(int first);
    }
}