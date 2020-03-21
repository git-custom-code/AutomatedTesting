namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded non-void method
    /// with value type parameters.
    /// </summary>
    public interface IFooFuncValueTypeOverloadsIn<T> : IFoo
        where T : struct
    {
        T MethodWithOverload(T first);

        T MethodWithOverload(T first, T second);
    }
}