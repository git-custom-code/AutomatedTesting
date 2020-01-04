namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked methods with
    /// <see cref="System.Action"/> signature.
    /// </summary>
    public sealed class ActionInvocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ActionInvocation"/> type.
        /// </summary>
        /// <param name="parameter"> The parameter names and passed values of the invoked method. </param>
        /// <param name="signature"> The signature of the invoked method (as <see cref="MethodInfo"/>). </param>
        public ActionInvocation(IDictionary<string, object> parameter, MethodInfo signature)
        {
            Parameter = (IReadOnlyDictionary<string, object>)parameter;
            Signature = signature;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the parameter names and passed values of the invoked method.
        /// </summary>
        public IReadOnlyDictionary<string, object> Parameter { get; }

        /// <summary>
        /// Gets the signature of the invoked method (as <see cref="MethodInfo"/>).
        /// </summary>
        public MethodInfo Signature { get; }

        #endregion
    }
}