namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a type with a single dependency.
    /// </summary>
    public interface IFoo<T> where T : IBar
    {
        T Dependency { get; }
    }
}