namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type indexed property getter.
    /// </summary>
    public interface IFooReferenceTypeIndexedGetter<T> : IFoo
        where T : class
    {
        T? this[T? i] { get; }
    }
}