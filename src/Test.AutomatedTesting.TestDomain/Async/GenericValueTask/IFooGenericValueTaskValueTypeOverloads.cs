namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded asynchronous method
    /// with value type parameters that return a (value type) <see cref="ValueTask{TResult}"/>.
    /// </summary>
    public interface IFooGenericValueTaskValueTypeOverloads : IFoo
    {
        ValueTask<int> MethodWithOverloadAsync(int first);

        ValueTask<int> MethodWithOverloadAsync(int first, double second);
    }
}