namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// with a reference type parameter that returns a <see cref="ValueTask"/>.
/// </summary>
public interface IFooValueTaskReferenceTypeParameter : IFoo
{
    ValueTask MethodWithOneParameterAsync(object? first);
}
