namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains void methods with reference type parameters.
    /// </summary>
    public interface IBarWithReferenceTypeAction : IBar
    {
        void MethodWithoutParameter();

        void MethodWithOneParameter(object? first);
    }
}