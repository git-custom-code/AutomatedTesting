namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns a <see cref="Task"/>.
    /// </summary>
    public interface IFooTaskValueTypeParameterless : IFoo
    {
        Task MethodWithoutParameterAsync();
    }
}