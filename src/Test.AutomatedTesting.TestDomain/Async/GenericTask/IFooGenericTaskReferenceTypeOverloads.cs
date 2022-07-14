namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded asynchronous method
/// with reference type parameters that return a (reference type) <see cref="Task{TResult}"/>.
/// </summary>
public interface IFooGenericTaskReferenceTypeOverloads : IFoo
{
    Task<object?> MethodWithOverloadAsync(object? first);

    Task<object?> MethodWithOverloadAsync(object? first, object? second);
}
