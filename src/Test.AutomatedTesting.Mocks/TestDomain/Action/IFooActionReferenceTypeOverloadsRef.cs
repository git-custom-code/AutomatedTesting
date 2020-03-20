namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded void method
    /// with reference type ref parameters.
    /// </summary>
    public interface IFooActionReferenceTypeOverloadsRef<T> : IFoo
        where T : class
    {
        void MethodWithOneParameter(ref T? first);

        void MethodWithOneParameter(ref T? first, ref T? second);
    }
}