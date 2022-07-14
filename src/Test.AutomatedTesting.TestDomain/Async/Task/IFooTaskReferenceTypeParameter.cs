namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// with a reference type parameter that returns a <see cref="Task"/>.
/// </summary>
public interface IFooTaskReferenceTypeParameter : IFoo
{
    Task MethodWithOneParameterAsync(object? first);
}
