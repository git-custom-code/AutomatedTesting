namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// with a value type parameter that returns a (value type) <see cref="Task{TResult}"/>.
    /// </summary>
    public interface IFooGenericTaskValueTypeParameter : IFoo
    {
        Task<int> MethodWithOneParameterAsync(int first);
    }
}