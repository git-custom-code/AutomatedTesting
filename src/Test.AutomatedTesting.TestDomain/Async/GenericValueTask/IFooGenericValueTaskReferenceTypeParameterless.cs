namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// without parameters that returns a (reference type) <see cref="ValueTask{TResult}"/>.
/// </summary>
public interface IFooGenericValueTaskReferenceTypeParameterless : IFoo
{
    ValueTask<object?> MethodWithoutParameterAsync();
}
