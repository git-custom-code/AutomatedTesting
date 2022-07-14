namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded asynchronous method
/// with value type parameters that return a <see cref="ValueTask"/>.
/// </summary>
public interface IFooValueTaskValueTypeOverloads : IFoo
{
    ValueTask MethodWithOverloadAsync(int first);

    ValueTask MethodWithOverloadAsync(int first, double second);
}
