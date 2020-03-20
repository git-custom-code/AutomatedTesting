namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns a (value type) <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public interface IFooGenericValueTaskValueTypeParameterless : IFoo
    {
        ValueTask<int> MethodWithoutParameterAsync();
    }
}