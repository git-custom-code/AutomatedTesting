namespace CustomCode.AutomatedTesting.Analyzer
{
    /// <summary>
    /// Type that encapsulates the name and code of an in-memory source code file.
    /// </summary>
    public sealed class SourceCodeFile
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="SourceCodeFile"/> type.
        /// </summary>
        /// <param name="name"> The name of the source code file. </param>
        /// <param name="code"> The c# source code. </param>
        public SourceCodeFile(string name, string code)
        {
            Name = name;
            Code = code;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the name of the source code file.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the c# source code.
        /// </summary>
        public string Code { get; }

        #endregion
    }
}