namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains a void method with a reference type parameter.
/// </summary>
public interface IFooActionReferenceTypeParameterIn<T> : IFoo
    where T : class
{
    void MethodWithOneParameter(T? first);
}
