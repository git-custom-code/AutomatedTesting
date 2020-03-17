namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
    using Interception;
    using Interception.Async;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading.Tasks;

    /// <summary>
    /// Default implementation of the <see cref="IMethodEmitterFactory"/> interface.
    /// </summary>
    public sealed class MethodEmitterFactory : IMethodEmitterFactory
    {
        #region Data

        /// <summary>
        /// Gets a thread-safe cache that is used to store factories for creating strongly typed
        /// <see cref="IMethodEmitter"/> instances for asynchronous methods.
        /// </summary>
        private static ConcurrentDictionary<Type, MethodEmitterDelegate> AsyncMethodEmitterCache { get; }
            = new ConcurrentDictionary<Type, MethodEmitterDelegate>();

        /// <summary>
        /// Gets a thread-safe cache that is used to store factories for creating strongly typed
        /// <see cref="IMethodEmitter"/> instances for methods with non-void return values.
        /// </summary>
        private static ConcurrentDictionary<Type, MethodEmitterDelegate> FuncMethodEmitterCache { get; }
            = new ConcurrentDictionary<Type, MethodEmitterDelegate>();

        #endregion

        #region Logic

        /// <inheritdoc />
        public IMethodEmitter CreateMethodEmitterFor(MethodInfo signature, TypeBuilder type, FieldBuilder interceptor)
        {
            if (signature.ReturnType == typeof(void))
            {
                return new InterceptActionEmitter(type, signature, interceptor);
            }

            if (signature.ReturnType == typeof(Task))
            {
                return new InterceptAsyncMethodEmitter<AsyncTaskInvocation>(type, signature, interceptor);
            }

            if (signature.ReturnType == typeof(ValueTask))
            {
                return new InterceptAsyncMethodEmitter<AsyncValueTaskInvocation>(type, signature, interceptor);
            }

            if (signature.ReturnType.IsGenericType)
            {
                var returnTypeSignature = signature.ReturnType.GetGenericTypeDefinition();
                if (returnTypeSignature == typeof(Task<>))
                {
                    var factory = AsyncMethodEmitterCache.GetOrAdd(
                        signature.ReturnType,
                        CreateAsyncMethodEmitterFactoryFor(signature.ReturnType, typeof(AsyncGenericTaskInvocation<>)));
                    return factory(type, signature, interceptor);
                }

                if (returnTypeSignature == typeof(ValueTask<>))
                {
                    var factory = AsyncMethodEmitterCache.GetOrAdd(
                        signature.ReturnType,
                        CreateAsyncMethodEmitterFactoryFor(signature.ReturnType, typeof(AsyncGenericValueTaskInvocation<>)));
                    return factory(type, signature, interceptor);
                }

                if (returnTypeSignature == typeof(IAsyncEnumerable<>))
                {
                    var factory = AsyncMethodEmitterCache.GetOrAdd(
                        signature.ReturnType,
                        CreateAsyncMethodEmitterFactoryFor(signature.ReturnType, typeof(AsyncIEnumerableInvocation<>)));
                    return factory(type, signature, interceptor);
                }
            }

            {
                var factory = FuncMethodEmitterCache.GetOrAdd(
                    signature.ReturnType,
                    CreateFuncMethodEmitterFactoryFor(signature.ReturnType));
                return factory(type, signature, interceptor);
            }
        }

        /// <summary>
        /// Use a <see cref="DynamicMethod"/> to create a factory delegate for a strongly typed
        /// <see cref="InterceptAsyncMethodEmitter{T}" />.
        /// </summary>
        /// <param name="asyncType">
        /// The return type of the asynchronous method (either <see cref="Task{TResult}"/>, <see cref="ValueTask{TResult}"/>
        /// or <see cref="IAsyncEnumerable{T}"/>).
        /// </param>
        /// <param name="featureType">
        /// The <see cref="IAsyncInvocation{T}"/> that is created by the emitter for each intercepted method invocation.
        /// </param>
        /// <returns> The created factory delegate. </returns>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     IMethodEmitter Create(TypeBuilder type, MethodInfo signature, FieldBuilder interceptor)
        ///     {
        ///         return new InterceptAsyncMethodEmitter<FeatureType<AsyncType>>(type, signature, interceptor);
        ///     }
        /// ]]>
        /// </remarks>
        private MethodEmitterDelegate CreateAsyncMethodEmitterFactoryFor(Type asyncType, Type featureType)
        {
            var genericFeatureType = featureType.MakeGenericType(asyncType.GetGenericArguments());
            var emitterType = typeof(InterceptAsyncMethodEmitter<>);
            var genericEmitterType = emitterType.MakeGenericType(genericFeatureType);
            var ctor = genericEmitterType
                .GetConstructor(new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder) })
                ?? throw new ConstructorInfoException(genericEmitterType);

            var dynamicFactory = new DynamicMethod(
                $"Create{genericEmitterType.Name}",
                typeof(IMethodEmitter),
                new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder) },
                genericEmitterType);
            var body = dynamicFactory.GetILGenerator();

            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldarg_1);
            body.Emit(OpCodes.Ldarg_2);
            body.Emit(OpCodes.Newobj, ctor);
            body.Emit(OpCodes.Ret);

            var factory = (MethodEmitterDelegate)dynamicFactory.CreateDelegate(typeof(MethodEmitterDelegate));
            return factory;
        }

        /// <summary>
        /// Use a <see cref="DynamicMethod"/> to create a factory delegate for a strongly typed
        /// <see cref="InterceptFuncEmitter{T}" />.
        /// </summary>
        /// <param name="returnType">
        /// The type of the function's return value.
        /// </param>
        /// <returns> The created factory delegate. </returns>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     IMethodEmitter Create(TypeBuilder type, MethodInfo signature, FieldBuilder interceptor)
        ///     {
        ///         return new InterceptFuncEmitter<ReturnType>(type, signature, interceptor);
        ///     }
        /// ]]>
        /// </remarks>
        private MethodEmitterDelegate CreateFuncMethodEmitterFactoryFor(Type returnType)
        {
            var emitterType = typeof(InterceptFuncEmitter<>);
            var genericEmitterType = emitterType.MakeGenericType(returnType);
            var ctor = genericEmitterType
                .GetConstructor(new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder) })
                ?? throw new ConstructorInfoException(genericEmitterType);

            var dynamicFactory = new DynamicMethod(
                $"Create{genericEmitterType.Name}",
                typeof(IMethodEmitter),
                new[] { typeof(TypeBuilder), typeof(MethodInfo), typeof(FieldBuilder) },
                genericEmitterType);
            var body = dynamicFactory.GetILGenerator();

            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldarg_1);
            body.Emit(OpCodes.Ldarg_2);
            body.Emit(OpCodes.Newobj, ctor);
            body.Emit(OpCodes.Ret);

            var factory = (MethodEmitterDelegate)dynamicFactory.CreateDelegate(typeof(MethodEmitterDelegate));
            return factory;
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// A delegate that defines a factory for creating a strongly typed <see cref="InterceptAsyncMethodEmitter{T}"/>
        /// instance.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the asynchronous method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        /// <returns> The created <see cref="InterceptAsyncMethodEmitter{T}"/> instance. </returns>
        private delegate IMethodEmitter MethodEmitterDelegate(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField);

        #endregion
    }
}