namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type indexed property.
    /// </summary>
    public interface IFooValueTypeIndexedProperty<T> : IFoo
        where T : struct
    {
        T this[T first] { get; set; }
    }
}