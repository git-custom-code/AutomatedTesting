namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a reference type property.
    /// </summary>
    public interface IFooReferenceTypeProperty<T> : IFoo
        where T : class
    {
        T? GetterSetter { get; set; }
    }
}