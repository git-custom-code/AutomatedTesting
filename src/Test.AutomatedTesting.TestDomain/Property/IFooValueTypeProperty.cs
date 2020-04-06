namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type property.
    /// </summary>
    public interface IFooValueTypeProperty<T> : IFoo
        where T : struct
    {
        T GetterSetter { get; set; }
    }
}