namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded asynchronous method
    /// with value type parameters that return a <see cref="Task"/>.
    /// </summary>
    public interface IFooTaskValueTypeOverloads : IFoo
    {
        Task MethodWithOverloadAsync(int first);

        Task MethodWithOverloadAsync(int first, double second);
    }
}