namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns a (value type) <see cref="Task{TResult}"/>.
    /// </summary>
    public interface IFooGenericTaskValueTypeParameterless : IFoo
    {
        Task<int> MethodWithoutParameterAsync();
    }
}