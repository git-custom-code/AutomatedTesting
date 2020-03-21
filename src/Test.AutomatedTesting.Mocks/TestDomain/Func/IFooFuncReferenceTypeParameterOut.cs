namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a reference type out parameter.
    /// </summary>
    public interface IFooFuncReferenceTypeParameterOut<T> : IFoo
        where T : class
    {
        T? MethodWithOneParameter(out T? first);
    }
}