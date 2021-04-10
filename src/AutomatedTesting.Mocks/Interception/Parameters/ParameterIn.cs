namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using ExceptionHandling;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for intercepted input parameters.
    /// </summary>
    public sealed class ParameterIn : IParameterIn
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ParameterIn"/> type.
        /// </summary>
        /// <param name="signature"> The intercepted method's signature. </param>
        /// <param name="values"> The intercepted method's in(put) parameters. </param>
        public ParameterIn(MethodInfo signature, object?[] values)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(values, nameof(values));

            var inputParameter = new List<Parameter>();
            var methodParameter = signature.GetParameters();
            for (var i=0; i<values.Length; ++i)
            {
                var parameter = methodParameter[i];
                if (!parameter.IsOut && !parameter.ParameterType.IsByRef)
                {
                    var value = values[i];
                    inputParameter.Add(new Parameter(
                        parameter.Name ?? "unknown",
                        parameter.ParameterType,
                        value));
                }
            }
            InputParameterCollection = inputParameter;
        }

        #endregion

        #region Data

        /// <inheritdoc cref="IParameterIn" />
        public IEnumerable<Parameter> InputParameterCollection { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"{InputParameterCollection.Count()} input parameter";
        }

        #endregion
    }
}