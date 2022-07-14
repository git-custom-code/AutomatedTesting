namespace CustomCode.AutomatedTesting.TestDomain;

using System.Collections.Generic;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// with a reference type parameter that returns an <see cref="IAsyncEnumerable{T}"/> with reference types.
/// </summary>
public interface IFooAsyncEnumerableReferenceTypeParameter : IFoo
{
    IAsyncEnumerable<object?> MethodWithOneParameterAsync(object? first);
}
