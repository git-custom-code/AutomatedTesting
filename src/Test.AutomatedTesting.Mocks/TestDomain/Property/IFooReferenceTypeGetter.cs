namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type property getter.
    /// </summary>
    public interface IFooReferenceTypeGetter<T> : IFoo
        where T : class
    {
        T? Getter { get; }
    }
}