namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains a void method without parameters.
/// </summary>
public interface IFooActionParameterless : IFoo
{
    void MethodWithoutParameter();
}
