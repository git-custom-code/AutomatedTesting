namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded asynchronous method
    /// with reference type parameters that return an <see cref="IAsyncEnumerable{T}"/> with reference types.
    /// </summary>
    public interface IFooAsyncEnumerableReferenceTypeOverloads : IFoo
    {
        IAsyncEnumerable<object?> MethodWithOverloadAsync(object? first);

        IAsyncEnumerable<object?> MethodWithOverloadAsync(object? first, object? second);
    }
}