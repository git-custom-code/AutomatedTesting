namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with value type out parameters.
    /// </summary>
    public interface IFooActionValueTypeOverloadsOut<T> : IFoo
        where T : struct
    {
        void MethodWithOverload(out T first);

        void MethodWithOverload(out T first, out T second);
    }
}