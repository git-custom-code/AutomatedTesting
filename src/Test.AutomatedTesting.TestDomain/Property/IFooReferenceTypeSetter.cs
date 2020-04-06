namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type property setter.
    /// </summary>
    public interface IFooReferenceTypeSetter<T> : IFoo
        where T : class
    {
        T? Setter { set; }
    }
}