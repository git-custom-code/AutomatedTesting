namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Default implementation of the <see cref="IDependencyEmitter"/> interface.
    /// </summary>
    public sealed class DependencyEmitter : IDependencyEmitter
    {
        #region Logic

        /// <inheritdoc cref="IDependencyEmitter" />
        public FieldBuilder CreateInterceptorDependency(TypeBuilder type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var field = type.DefineField(
                "_interceptor",
                typeof(IInterceptor),
                FieldAttributes.Private | FieldAttributes.InitOnly);
            return field;
        }

        /// <inheritdoc cref="IDependencyEmitter" />
        public FieldBuilder CreateDecorateeDependency(TypeBuilder type, Type decorateeType)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (decorateeType == null)
            {
                throw new ArgumentNullException(nameof(decorateeType));
            }

            var field = type.DefineField(
                "_decoratee",
                decorateeType,
                FieldAttributes.Private | FieldAttributes.InitOnly);
            return field;
        }

        /// <inheritdoc cref="IDependencyEmitter" />
        public void CreateConstructor(TypeBuilder type, params FieldBuilder[] dependencies)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var constructor = type.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                dependencies.Select(d => d.FieldType).ToArray());

            var body = constructor.GetILGenerator();
            var baseCtor = typeof(object).GetConstructor(Array.Empty<Type>());
            if (baseCtor != null)
            {
                body.Emit(OpCodes.Ldarg_0);
                body.Emit(OpCodes.Call, baseCtor);
                body.Emit(OpCodes.Nop);
                body.Emit(OpCodes.Nop);
            }

            if (dependencies != null)
            {
                for (var i = 0u; i < dependencies.Length; ++i)
                {
                    body.Emit(OpCodes.Ldarg_0);
                    body.Emit(OpCodes.Ldarg_S, i + 1);
                    body.Emit(OpCodes.Stfld, dependencies[i]);
                }
            }

            body.Emit(OpCodes.Ret);
        }

        #endregion
    }
}