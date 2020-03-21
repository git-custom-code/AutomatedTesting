namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a value type out parameter.
    /// </summary>
    public interface IFooFuncValueTypeParameterOut<T> : IFoo
        where T : struct
    {
        T MethodWithOneParameter(out T first);
    }
}