namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// with a value type parameter that returns a (value type) <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public interface IFooGenericValueTaskValueTypeParameter : IFoo
    {
        ValueTask<int> MethodWithOneParameterAsync(int first);
    }
}