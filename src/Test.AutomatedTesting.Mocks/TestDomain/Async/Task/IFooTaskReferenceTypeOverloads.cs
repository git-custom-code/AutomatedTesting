namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded asynchronous method
    /// with reference type parameters that return a <see cref="Task"/>.
    /// </summary>
    public interface IFooTaskReferenceTypeOverloads : IFoo
    {
        Task MethodWithOverloadAsync(object? first);

        Task MethodWithOverloadAsync(object? first, object? second);
    }
}