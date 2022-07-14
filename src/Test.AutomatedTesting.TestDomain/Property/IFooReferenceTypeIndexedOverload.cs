namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains a reference type indexed property
/// with an addional overload.
/// </summary>
public interface IFooReferenceTypeIndexedOverload<T> : IFoo
    where T : class
{
    T? this[T? first] { get; set; }

    T? this[T? first, T? second] { get; set; }
}
