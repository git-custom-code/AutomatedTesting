namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded asynchronous method
    /// with reference type parameters that return a <see cref="ValueTask"/>.
    /// </summary>
    public interface IFooValueTaskReferenceTypeOverloads : IFoo
    {
        ValueTask MethodWithOverloadAsync(object? first);

        ValueTask MethodWithOverloadAsync(object? first, object? second);
    }
}