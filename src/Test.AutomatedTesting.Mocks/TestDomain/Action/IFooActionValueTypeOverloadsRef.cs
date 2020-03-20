namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with value type ref parameters.
    /// </summary>
    public interface IFooActionValueTypeOverloadsRef<T> : IFoo
        where T : struct
    {
        void MethodWithOneParameter(ref T first);

        void MethodWithOneParameter(ref T first, ref T second);
    }
}