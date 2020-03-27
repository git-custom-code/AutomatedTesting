namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IMethodDecoratorEmitterFactory"/> interface for
    /// emitting method decorators.
    /// </summary>
    public sealed class MethodDecoratorEmitterFactory : IMethodDecoratorEmitterFactory
    {
        #region Logic

        /// <inheritdoc />
        public IMethodEmitter CreateMethodDecoratorEmitterFor(
            MethodInfo signature,
            TypeBuilder type,
            FieldBuilder decoratee,
            FieldBuilder interceptor)
        {
            return new DecorateActionEmitter(type, signature, decoratee, interceptor);
        }

        #endregion
    }
}