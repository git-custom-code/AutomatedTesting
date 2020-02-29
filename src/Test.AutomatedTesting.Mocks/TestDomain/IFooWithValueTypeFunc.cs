namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains methods that return value type values.
    /// </summary>
    public interface IFooWithValueTypeFunc : IFoo
    {
        int MethodWithoutParameter();

        int MethodWithOneParameter(int first);
    }
}