namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded void method
/// with reference type ref parameters.
/// </summary>
public interface IFooActionReferenceTypeOverloadsRef<T> : IFoo
    where T : class
{
    void MethodWithOverload(ref T? first);

    void MethodWithOverload(ref T? first, ref T? second);
}
