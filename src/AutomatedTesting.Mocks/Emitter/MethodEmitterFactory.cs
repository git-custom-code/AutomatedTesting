using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    /// <summary>
    /// Default implementation of the <see cref="IMethodEmitterFactory"/> interface.
    /// </summary>
    public sealed class MethodEmitterFactory : IMethodEmitterFactory
    {
        #region Logic

        /// <inheritdoc />
        public IMethodEmitter CreateMethodEmitterFor(MethodInfo signature, TypeBuilder type, FieldBuilder interceptor)
        {
            if (signature.ReturnType == typeof(void))
            {
                if (signature.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef) == false)
                {
                    return default;
                }
            }

            throw new System.NotImplementedException();
        }

        #endregion
    }
}