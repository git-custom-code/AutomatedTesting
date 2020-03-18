namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains asynchronous methods that return void.
    /// </summary>
    public interface IFooWithAsyncValueTypeAction : IFoo
    {
        Task MethodWithoutParameterAsync();

        Task MethodWithOneParameterAsync(int first);
    }
}