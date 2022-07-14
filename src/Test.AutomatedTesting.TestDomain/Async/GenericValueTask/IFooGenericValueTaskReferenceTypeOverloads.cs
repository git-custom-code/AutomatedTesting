namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded asynchronous method
/// with reference type parameters that return a (reference type) <see cref="ValueTask{TResult}"/>.
/// </summary>
public interface IFooGenericValueTaskReferenceTypeOverloads : IFoo
{
    ValueTask<object?> MethodWithOverloadAsync(object? first);

    ValueTask<object?> MethodWithOverloadAsync(object? first, object? second);
}
