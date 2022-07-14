namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded asynchronous method
/// with value type parameters that return a (value type) <see cref="Task{TResult}"/>.
/// </summary>
public interface IFooGenericTaskValueTypeOverloads : IFoo
{
    Task<int> MethodWithOverloadAsync(int first);

    Task<int> MethodWithOverloadAsync(int first, double second);
}
