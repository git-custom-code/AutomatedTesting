namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded non-void method
/// with value type out parameters.
/// </summary>
public interface IFooFuncValueTypeOverloadsOut<T> : IFoo
    where T : struct
{
    T MethodWithOverload(out T first);

    T MethodWithOverload(out T first, out T second);
}
