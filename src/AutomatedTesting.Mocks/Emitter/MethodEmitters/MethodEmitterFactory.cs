namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading.Tasks;

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
                    return new InterceptActionEmitter(type, signature, interceptor);
                }
            }
            else
            {
                if (signature.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef) == false)
                {
                    if (signature.ReturnType == typeof(Task))
                    {
                        return new InterceptAsyncActionEmitter(type, signature, interceptor);
                    }
                    else if (signature.ReturnType.IsGenericType && signature.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        return new InterceptAsyncFuncEmitter(type, signature, interceptor);
                    }
                    else
                    {
                        return new InterceptFuncEmitter(type, signature, interceptor);
                    }
                }
            }

            throw new NotImplementedException();
        }

        #endregion
    }
}