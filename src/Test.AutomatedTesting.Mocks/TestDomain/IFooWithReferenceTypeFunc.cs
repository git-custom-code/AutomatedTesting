namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains methods that return reference type values.
    /// </summary>
    public interface IFooWithReferenceTypeFunc : IFoo
    {
        object? MethodWithoutParameter();

        object? MethodWithOneParameter(object? first);
    }
}