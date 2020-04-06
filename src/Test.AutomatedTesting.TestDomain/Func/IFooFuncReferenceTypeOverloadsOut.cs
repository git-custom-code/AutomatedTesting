namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains an overloaded non-void method
    /// with reference type out parameters.
    /// </summary>
    public interface IFooFuncReferenceTypeOverloadsOut<T> : IFoo
        where T : class
    {
        T? MethodWithOverload(out T? first);

        T? MethodWithOverload(out T? first, out T? second);
    }
}