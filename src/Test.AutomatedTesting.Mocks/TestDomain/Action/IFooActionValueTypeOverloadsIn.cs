namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with value type parameters.
    /// </summary>
    public interface IFooActionValueTypeOverloadsIn<T> : IFoo
        where T : struct
    {
        void MethodWithOneParameter(T first);

        void MethodWithOneParameter(T first, T second);
    }
}