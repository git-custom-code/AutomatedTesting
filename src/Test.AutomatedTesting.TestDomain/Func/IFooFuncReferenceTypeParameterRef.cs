namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a non-void method
    /// with a reference type ref parameter.
    /// </summary>
    public interface IFooFuncReferenceTypeParameterRef<T> : IFoo
        where T : class
    {
        T? MethodWithOneParameter(ref T? first);
    }
}