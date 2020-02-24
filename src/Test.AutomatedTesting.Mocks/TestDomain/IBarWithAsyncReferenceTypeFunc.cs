namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains asynchronous methods that return reference type values.
    /// </summary>
    public interface IBarWithAsyncReferenceTypeFunc : IBar
    {
        Task<object?> MethodWithoutParameterAsync();

        Task<object?> MethodWithOneParameterAsync(object? first);
    }
}