namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded non-void method
    /// with reference type ref parameters.
    /// </summary>
    public interface IFooFuncReferenceTypeOverloadsRef<T> : IFoo
        where T : class
    {
        T? MethodWithOverload(ref T? first);

        T? MethodWithOverload(ref T? first, ref T? second);
    }
}