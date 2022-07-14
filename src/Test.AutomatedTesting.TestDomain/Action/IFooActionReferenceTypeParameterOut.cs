namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains a void method with a reference type out parameter.
/// </summary>
public interface IFooActionReferenceTypeParameterOut<T> : IFoo
    where T : class
{
    void MethodWithOneParameter(out T? first);
}
