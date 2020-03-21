namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a value type parameter.
    /// </summary>
    public interface IFooFuncValueTypeParameterIn<T> : IFoo
        where T : struct
    {
        T MethodWithOneParameter(T first);
    }
}