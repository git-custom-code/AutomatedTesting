namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using ExceptionHandling;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for intercepted out parameters.
    /// </summary>
    public sealed class ParameterOut : IParameterOut
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ParameterOut"/> type.
        /// </summary>
        /// <param name="signature"> The intercepted method's signature. </param>
        public ParameterOut(MethodInfo signature)
        {
            var outParameter = new List<Parameter>();
            var methodParameter = signature.GetParameters();
            for (var i=0; i<methodParameter.Length; ++i)
            {
                var parameter = methodParameter[i];
                if (parameter.IsOut)
                {
                    var type = parameter.ParameterType.GetElementType()
                        ?? throw new MethodInfoException(typeof(Type), nameof(Type.GetElementType));
                    object? defaultValue = null;
                    if (type.IsValueType)
                    {
                        /*
                        var block = Expression.Convert(Expression.New(type), typeof(object));
                        var factory = Expression.Lambda<Func<object>>(block).Compile();
                        defaultValue = factory();
                         */

                        defaultValue = Activator.CreateInstance(type);
                    }

                    outParameter.Add(new Parameter(
                        parameter.Name ?? "unknown",
                        type,
                        defaultValue));
                }
            }
            OutParameterCollection = outParameter;
        }

        #endregion

        #region Data

        /// <inheritdoc />
        public IEnumerable<Parameter> OutParameterCollection { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        [return: MaybeNull]
        public T GetValue<T>(string name)
        {
            var parameter = OutParameterCollection
                .Single(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            return (T)parameter.Value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{OutParameterCollection.Count()} out parameter";
        }

        #endregion
    }
}