namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with reference type parameters.
    /// </summary>
    public interface IFooActionReferenceTypeOverloadsIn<T> : IFoo
        where T : class
    {
        void MethodWithOneParameter(T? first);

        void MethodWithOneParameter(T? first, T? second);
    }
}