namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a value type ref parameter.
    /// </summary>
    public interface IFooFuncValueTypeParameterRef<T> : IFoo
        where T : struct
    {
        T MethodWithOneParameter(ref T first);
    }
}