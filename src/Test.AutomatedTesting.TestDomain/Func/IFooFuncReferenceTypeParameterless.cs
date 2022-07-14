namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a reference type dependency that contains
/// a non-void method without parameters.
/// </summary>
public interface IFooFuncReferenceTypeParameterless<T> : IFoo
    where T : class
{
    T? MethodWithoutParameter();
}
