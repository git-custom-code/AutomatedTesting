namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked asynchroous methods with
    /// return type <see cref="Task"/>.
    /// </summary>
    public sealed class AsyncActionInvocation : IInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncActionInvocation"/> type.
        /// </summary>
        /// <param name="parameter"> The parameter signatures and passed values of the invoked asynchronous method. </param>
        /// <param name="signature"> The signature of the invoked asynchronous method (as <see cref="MethodInfo"/>). </param>
        public AsyncActionInvocation(IDictionary<ParameterInfo, object> parameter, MethodInfo signature)
        {
            Parameter = (IReadOnlyDictionary<ParameterInfo, object>)parameter;
            ReturnValue = Task.CompletedTask;
            Signature = signature;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the parameter signatures and passed values of the invoked asynchronous method.
        /// </summary>
        public IReadOnlyDictionary<ParameterInfo, object> Parameter { get; }

        /// <summary>
        /// Gets or sets the return value (task) of the intercepted asynchronous method.
        /// </summary>
        public Task ReturnValue { get; set; }

        /// <summary>
        /// Gets the signature of the invoked asynchronous method (as <see cref="MethodInfo"/>).
        /// </summary>
        public MethodInfo Signature { get; }

        #endregion
    }
}