namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains asynchronous methods that return value type values.
    /// </summary>
    public interface IFooWithAsyncValueTypeFunc : IFoo
    {
        Task<int> MethodWithoutParameterAsync();

        Task<int> MethodWithOneParameterAsync(int first);
    }
}