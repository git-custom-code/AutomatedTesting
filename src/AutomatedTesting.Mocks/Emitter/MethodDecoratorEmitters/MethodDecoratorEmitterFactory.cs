namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using CustomCode.AutomatedTesting.Mocks.ExceptionHandling;
    using Interception;
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IMethodDecoratorEmitterFactory"/> interface for
    /// emitting method decorators.
    /// </summary>
    public sealed class MethodDecoratorEmitterFactory : IMethodDecoratorEmitterFactory
    {
        #region Data

        /// <summary>
        /// Gets a thread-safe cache that is used to store factories for creating strongly typed
        /// <see cref="IMethodEmitter"/> instances for methods with non-void return values.
        /// </summary>
        private static ConcurrentDictionary<Type, MethodDecoratorEmitterDelegate> DecorateFuncEmitterCache { get; }
            = new ConcurrentDictionary<Type, MethodDecoratorEmitterDelegate>();

        #endregion

        #region Logic

        /// <inheritdoc />
        public IMethodEmitter CreateMethodDecoratorEmitterFor(
            MethodInfo signature,
            TypeBuilder type,
            FieldBuilder decoratee,
            FieldBuilder interceptor)
        {
            if (signature.ReturnType == typeof(void))
            {
                return new DecorateActionEmitter(type, signature, decoratee, interceptor);
            }

            {
                var factory = DecorateFuncEmitterCache.GetOrAdd(
                    signature.ReturnType,
                    CreateDecorateFuncEmitterFactoryFor(signature.ReturnType));
                return factory(type, signature, decoratee, interceptor);
            }
        }

        /// <summary>
        /// Use a <see cref="DynamicMethod"/> to create a factory delegate for a strongly typed
        /// <see cref="DecorateFuncEmitter{T}" />.
        /// </summary>
        /// <param name="returnType"> The type of the function's return value. </param>
        /// <returns> The created factory delegate. </returns>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     IMethodEmitter Create(TypeBuilder type, MethodInfo signature, FieldBuilder decoratee, FieldBuilder interceptor)
        ///     {
        ///         return new DecorateFuncEmitter<ReturnType>(type, signature, decoratee, interceptor);
        ///     }
        /// ]]>
        /// </remarks>
        private MethodDecoratorEmitterDelegate CreateDecorateFuncEmitterFactoryFor(Type returnType)
        {
            var emitterType = typeof(DecorateFuncEmitter<>);
            var genericEmitterType = emitterType.MakeGenericType(returnType);
            var ctor = genericEmitterType
                .GetConstructor(new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder), typeof(FieldBuilder) })
                ?? throw new ConstructorInfoException(genericEmitterType);

            var dynamicFactory = new DynamicMethod(
                $"Create{genericEmitterType.Name}",
                typeof(IMethodEmitter),
                new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder), typeof(FieldBuilder) },
                genericEmitterType);
            var body = dynamicFactory.GetILGenerator();

            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldarg_1);
            body.Emit(OpCodes.Ldarg_2);
            body.Emit(OpCodes.Ldarg_3);
            body.Emit(OpCodes.Newobj, ctor);
            body.Emit(OpCodes.Ret);

            var factory = (MethodDecoratorEmitterDelegate)dynamicFactory.CreateDelegate(typeof(MethodDecoratorEmitterDelegate));
            return factory;
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// A delegate that defines a factory for creating a strongly typed "Decorate...Emitter{T}".
        /// instance.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the asynchronous method to be created. </param>
        /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        /// <returns> The created <see cref="InterceptAsyncMethodEmitter{T}"/> instance. </returns>
        private delegate IMethodEmitter MethodDecoratorEmitterDelegate(
            TypeBuilder type,
            MethodInfo signature,
            FieldBuilder decorateeField,
            FieldBuilder interceptorField);

        #endregion
    }
}