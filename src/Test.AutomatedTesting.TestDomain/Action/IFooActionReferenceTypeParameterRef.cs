namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a void method with a reference type ref parameter.
    /// </summary>
    public interface IFooActionReferenceTypeParameterRef<T> : IFoo
        where T : class
    {
        void MethodWithOneParameter(ref T? first);
    }
}