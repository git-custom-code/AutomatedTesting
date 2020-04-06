namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// with a value type parameter that returns a <see cref="ValueTask"/>.
    /// </summary>
    public interface IFooValueTaskValueTypeParameter : IFoo
    {
        ValueTask MethodWithOneParameterAsync(int first);
    }
}