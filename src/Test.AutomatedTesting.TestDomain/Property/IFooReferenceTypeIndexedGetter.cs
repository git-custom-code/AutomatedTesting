namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains a reference type indexed property getter.
/// </summary>
public interface IFooReferenceTypeIndexedGetter<T> : IFoo
    where T : class
{
    T? this[T? first] { get; }
}
