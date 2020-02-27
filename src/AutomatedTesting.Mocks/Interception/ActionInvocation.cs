namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked methods with
    /// <see cref="System.Action"/> signature.
    /// </summary>
    public sealed class ActionInvocation : IInvocation, IHasInputParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ActionInvocation"/> type.
        /// </summary>
        /// <param name="parameter"> The parameter signatures and passed values of the invoked method. </param>
        /// <param name="signature"> The signature of the invoked method (as <see cref="MethodInfo"/>). </param>
        public ActionInvocation(IDictionary<ParameterInfo, object?> parameter, MethodInfo signature)
        {
            InputParameter = parameter.Select(p => (p.Key.ParameterType, p.Value)).ToArray();
            Signature = signature;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public IEnumerable<(Type type, object? value)> InputParameter { get; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        #endregion
    }
}