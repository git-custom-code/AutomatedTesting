namespace CustomCode.Analyzer.AutomatedTesting.Mocks.Shared
{
    /// <summary>
    /// Type that contains shared string constants.
    /// </summary>
    public sealed class Constants
    {
        #region Data

        /// <summary>
        /// The name of the "Mock" type.
        /// </summary>
        public const string TypeNameMock = "Mock";

        /// <summary>
        /// The name of the "Mock.CreateMocked" method.
        /// </summary>
        public const string MethodNameCreateMocked = "CreateMocked";

        /// <summary>
        /// The name of the "CustomCode.AutomatedTesting.Mocks" namespace.
        /// </summary>
        public const string NamespaceMocks = "CustomCode.AutomatedTesting.Mocks";

        /// <summary>
        /// The shared category that is used by all analyzers.
        /// </summary>
        public const string Category = "AutomatedTesting.Mocks";

        #endregion
    }
}