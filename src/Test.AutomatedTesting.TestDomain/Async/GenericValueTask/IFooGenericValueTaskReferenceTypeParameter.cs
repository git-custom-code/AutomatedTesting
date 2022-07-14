namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// with a reference type parameter that returns a (reference type) <see cref="ValueTask{TResult}"/>.
/// </summary>
public interface IFooGenericValueTaskReferenceTypeParameter : IFoo
{
    ValueTask<object?> MethodWithOneParameterAsync(object? first);
}
