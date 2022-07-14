namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a dependency that contains an overloaded void method
/// with value type ref parameters.
/// </summary>
public interface IFooActionValueTypeOverloadsRef<T> : IFoo
    where T : struct
{
    void MethodWithOverload(ref T first);

    void MethodWithOverload(ref T first, ref T second);
}
