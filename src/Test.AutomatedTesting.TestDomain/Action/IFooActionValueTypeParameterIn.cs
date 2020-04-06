namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a void method with a value type parameter.
    /// </summary>
    public interface IFooActionValueTypeParameterIn<T> : IFoo
        where T : struct
    {
        void MethodWithOneParameter(T first);
    }
}