namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type indexed property.
    /// </summary>
    public interface IFooReferenceTypeIndexedProperty<T> : IFoo
        where T : class
    {
        T? this[T? first] { get; set; }
    }
}