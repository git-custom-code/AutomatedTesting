namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded asynchronous method
/// with value type parameters that return an <see cref="IAsyncEnumerable{T}"/> with value types.
/// </summary>
public interface IFooAsyncEnumerableValueTypeOverloads : IFoo
{
    IAsyncEnumerable<int> MethodWithOverloadAsync(int first);

    IAsyncEnumerable<int> MethodWithOverloadAsync(int first, double second);
}
