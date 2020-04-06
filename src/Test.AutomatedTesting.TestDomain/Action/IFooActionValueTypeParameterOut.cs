namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a void method with a value type out parameter.
    /// </summary>
    public interface IFooActionValueTypeParameterOut<T> : IFoo
        where T : struct
    {
        void MethodWithOneParameter(out T first);
    }
}