namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of the <see cref="IInvocation"/> interface for invoked asynchronous methods with
    /// return type <see cref="Task"/>.
    /// </summary>
    public sealed class AsyncActionInvocation : IInvocation, IHasInputParameter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="AsyncActionInvocation"/> type.
        /// </summary>
        /// <param name="parameter"> The parameter signatures and passed values of the invoked asynchronous method. </param>
        /// <param name="signature"> The signature of the invoked asynchronous method (as <see cref="MethodInfo"/>). </param>
        public AsyncActionInvocation(IDictionary<ParameterInfo, object?> parameter, MethodInfo signature)
        {
            InputParameter = parameter.Select(p => (p.Key.ParameterType, p.Value)).ToArray();
            ReturnValue = Task.CompletedTask;
            Signature = signature;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public IEnumerable<(Type type, object? value)> InputParameter { get; }

        /// <summary>
        /// Gets or sets the return value (task) of the intercepted asynchronous method.
        /// </summary>
        public Task ReturnValue { get; set; }

        /// <inheritdoc />
        public MethodInfo Signature { get; }

        #endregion
    }
}