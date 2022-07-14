namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded non-void method
/// with reference type parameters.
/// </summary>
public interface IFooFuncReferenceTypeOverloadsIn<T> : IFoo
    where T : class
{
    T? MethodWithOverload(T? first);

    T? MethodWithOverload(T? first, T? second);
}
