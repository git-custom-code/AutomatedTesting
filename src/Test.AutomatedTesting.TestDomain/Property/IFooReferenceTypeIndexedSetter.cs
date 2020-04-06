namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type indexed property setter.
    /// </summary>
    public interface IFooReferenceTypeIndexedSetter<T> : IFoo
        where T : class
    {
        T? this[T? first] { set; }
    }
}