namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type indexed property setter.
    /// </summary>
    public interface IFooReferenceTypeIndexedSetter<T> : IFoo
        where T : class
    {
        T? this[T? i] { set; }
    }
}