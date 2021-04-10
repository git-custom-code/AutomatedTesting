namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using ExceptionHandling;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for intercepted ref parameters.
    /// </summary>
    public sealed class ParameterRef : IParameterRef
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ParameterRef"/> type.
        /// </summary>
        /// <param name="signature"> The intercepted method's signature. </param>
        /// <param name="values"> The intercepted method's ref parameters. </param>
        public ParameterRef(MethodInfo signature, object?[] values)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(values, nameof(values));

            var refParameter = new List<Parameter>();
            var methodParameter = signature.GetParameters();
            for (var i=0; i<methodParameter.Length; ++i)
            {
                var parameter = methodParameter[i];
                if (parameter.ParameterType.IsByRef)
                {
                    var value = values[i];
                    var type = parameter.ParameterType.GetElementType()
                        ?? throw new MethodInfoException(typeof(Type), nameof(Type.GetElementType));
                    refParameter.Add(new Parameter(
                        parameter.Name ?? "unknown",
                        type,
                        value));
                }
            }
            RefParameterCollection = refParameter;
        }

        #endregion

        #region Data

        /// <inheritdoc cref="IParameterRef" />
        public IEnumerable<Parameter> RefParameterCollection { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="IParameterRefOrOut" />
        [return: MaybeNull]
        public T GetValue<T>(string name)
        {
            var parameter = RefParameterCollection
                .Single(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
#nullable disable
            return (T)parameter.Value;
#nullable restore
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"{RefParameterCollection.Count()} ref parameter";
        }

        #endregion
    }
}