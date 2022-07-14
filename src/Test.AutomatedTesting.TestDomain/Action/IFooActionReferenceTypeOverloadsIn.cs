namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded void method
/// with reference type parameters.
/// </summary>
public interface IFooActionReferenceTypeOverloadsIn<T> : IFoo
    where T : class
{
    void MethodWithOverload(T? first);

    void MethodWithOverload(T? first, T? second);
}
