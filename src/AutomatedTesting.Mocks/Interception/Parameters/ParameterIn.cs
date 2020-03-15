namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
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
        /// <param name="signature"></param>
        /// <param name="values"></param>
        public ParameterIn(MethodInfo signature, object?[] values)
        {
            var inputParameter = new List<Parameter>();
            var methodParameter = signature.GetParameters();
            for (var i=0; i<methodParameter.Length; ++i)
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

        /// <inheritdoc />
        public IEnumerable<Parameter> InputParameterCollection { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{InputParameterCollection.Count()} input parameter";
        }

        #endregion
    }
}