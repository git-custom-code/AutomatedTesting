namespace CustomCode.AutomatedTesting.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an asynchronous method
    /// without parameters that returns a <see cref="ValueTask"/>.
    /// </summary>
    public interface IFooValueTaskParameterless : IFoo
    {
        ValueTask MethodWithoutParameterAsync();
    }
}