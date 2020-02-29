namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains void methods with reference type parameters.
    /// </summary>
    public interface IFooWithReferenceTypeAction : IFoo
    {
        void MethodWithoutParameter();

        void MethodWithOneParameter(object? first);
    }
}