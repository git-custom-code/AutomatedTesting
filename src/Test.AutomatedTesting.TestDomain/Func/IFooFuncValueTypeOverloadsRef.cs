namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded non-void method
/// with value type ref parameters.
/// </summary>
public interface IFooFuncValueTypeOverloadsRef<T> : IFoo
    where T : struct
{
    T MethodWithOverload(ref T first);

    T MethodWithOverload(ref T first, ref T second);
}
