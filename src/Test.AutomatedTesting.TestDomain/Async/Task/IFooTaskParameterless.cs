namespace CustomCode.AutomatedTesting.TestDomain;

using System.Threading.Tasks;

/// <summary>
/// Interface that simulates a dependency that contains an asynchronous method
/// without parameters that returns a <see cref="Task"/>.
/// </summary>
public interface IFooTaskParameterless : IFoo
{
    Task MethodWithoutParameterAsync();
}
