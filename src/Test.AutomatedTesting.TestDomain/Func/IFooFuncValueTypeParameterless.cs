namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a value type dependency that contains
/// a non-void method without parameters.
/// </summary>
public interface IFooFuncValueTypeParameterless<T> : IFoo
    where T : struct
{
    T MethodWithoutParameter();
}
