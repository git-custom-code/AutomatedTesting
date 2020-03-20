namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a void method with a value type ref parameter.
    /// </summary>
    public interface IFooActionValueTypeParameterRef<T> : IFoo
        where T : struct
    {
        void MethodWithOneParameter(ref T first);
    }
}