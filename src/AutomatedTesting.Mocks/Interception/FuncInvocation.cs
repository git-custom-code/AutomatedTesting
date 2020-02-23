namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked methods with
    /// non-void return type signature.
    /// </summary>
    public sealed class FuncInvocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="FuncInvocation"/> type.
        /// </summary>
        /// <param name="parameter"> The parameter signatures and passed values of the invoked method. </param>
        /// <param name="signature"> The signature of the invoked method (as <see cref="MethodInfo"/>). </param>
        public FuncInvocation(IDictionary<ParameterInfo, object> parameter, MethodInfo signature)
        {
            Parameter = (IReadOnlyDictionary<ParameterInfo, object>)parameter;
            Signature = signature;
            ReturnValue = null;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the parameter signatures and passed values of the invoked method.
        /// </summary>
        public IReadOnlyDictionary<ParameterInfo, object> Parameter { get; }

        /// <summary>
        /// Gets or sets the return value of the intercepted method.
        /// </summary>
        public object? ReturnValue { get; set; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        #endregion
    }
}