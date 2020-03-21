namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a reference type parameter.
    /// </summary>
    public interface IFooFuncReferenceTypeParameterIn<T> : IFoo
        where T : class
    {
        T? MethodWithOneParameter(T? first);
    }
}