namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with reference type out parameters.
    /// </summary>
    public interface IFooActionReferenceTypeOverloadsOut<T> : IFoo
        where T : class
    {
        void MethodWithOverload(out T? first);

        void MethodWithOverload(out T? first, out T? second);
    }
}