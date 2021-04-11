namespace CustomCode.AutomatedTesting.Mocks
{
    /// <summary>
    /// Helper class that allows the declaration of inline out/ref parameters within expression trees.
    /// </summary>
    /// <typeparam name="T"> The type of the variable that should be declared. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    /// mocked.ArrangeFor<IFoo>().That(m => m.Execute(out Any<int>._, ref Any<int>._)
    /// ]]>
    /// </remarks>
    public static class Any<T>
    {
        /// <summary>
        /// The inline discard parameter.
        /// </summary>
#nullable disable
        public static T _;
#nullable restore
    }
}