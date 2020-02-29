namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains void methods with value type parameters.
    /// </summary>
    public interface IFooWithValueTypeAction : IFoo
    {
        void MethodWithoutParameter();

        void MethodWithOneParameter(int first);
    }
}