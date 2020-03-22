namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type property setter.
    /// </summary>
    public interface IFooValueTypeSetter<T> : IFoo
        where T : struct
    {
        T Setter { set; }
    }
}