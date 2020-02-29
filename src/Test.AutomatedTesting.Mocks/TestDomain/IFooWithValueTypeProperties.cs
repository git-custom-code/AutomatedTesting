namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains reference type properties.
    /// </summary>
    public interface IFooWithValueTypeProperties : IFoo
    {
        int Getter { get; }

        int Setter { set; }

        int GetterSetter { get; set; }
    }
}