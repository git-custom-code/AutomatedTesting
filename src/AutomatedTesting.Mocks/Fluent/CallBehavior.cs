namespace CustomCode.AutomatedTesting.Mocks
{
    using Arrangements;
    using System.Reflection;

    /// <summary>
    /// Default implementation of the <see cref="ICallBehavior"/> interface.
    /// </summary>
    public sealed class CallBehavior : CallBehaviorBase, ICallBehavior
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="CallBehavior"/> type.
        /// </summary>
        /// <param name="arrangements"> The collection of arrangements that have been made for the associated mock. </param>
        /// <param name="signature"> The mocked method or property setter call. </param>
        public CallBehavior(IArrangementCollection arrangements, MethodInfo signature)
            : base(arrangements, signature)
        { }

        #endregion
    }
}