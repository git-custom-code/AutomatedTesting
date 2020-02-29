namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains reference type properties.
    /// </summary>
    public interface IBarWithValueTypeProperties : IBar
    {
        int Getter { get; }

        int Setter { set; }

        int GetterSetter { get; set; }
    }
}