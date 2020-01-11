namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
    using System.Linq;
    using System.Reflection.Emit;

    /// <summary>
    /// Default implementation of the <see cref="ITypeEmitter"/> interface.
    /// </summary>
    public sealed class TypeEmitter : ITypeEmitter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="TypeEmitter"/> type.
        /// </summary>
        /// <param name="typeBuilder"> The internal <see cref="TypeBuilder"/> that is used to dynamically emit logic. </param>
        /// <param name="dependencyEmitter"> The emitter instance used for creating the type's dependencies. </param>
        /// <param name="methodEmitterFactory">
        /// A factory that can create <see cref="IMethodEmitter"/> instance based on a method's signature.
        /// </param>
        public TypeEmitter(
            TypeBuilder typeBuilder,
            IDependencyEmitter dependencyEmitter,
            IMethodEmitterFactory methodEmitterFactory)
        {
            Type = typeBuilder;
            Dependencies = dependencyEmitter;
            MethodEmitterFactory = methodEmitterFactory;
        }

        /// <summary>
        /// Gets the emitter instance used for creating the type's dependencies.
        /// </summary>
        private IDependencyEmitter Dependencies { get; }

        /// <summary>
        /// Get a factory that can create <see cref="IMethodEmitter"/> instance based on a method's signature.
        /// </summary>
        private IMethodEmitterFactory MethodEmitterFactory { get; }

        /// <summary>
        /// Gets the internal <see cref="TypeBuilder"/> that is used to dynamically emit logic.
        /// </summary>
        private TypeBuilder Type { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void ImplementInterface<T>() where T : class
        {
            var @interface = typeof(T);
            if (!@interface.IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{@interface.FullName}'");
            }

            var interceptorField = Dependencies.CreateInterceptorDependency(Type);
            Dependencies.CreateConstructor(Type, interceptorField);
            Type.AddInterfaceImplementation(@interface);

            foreach (var signature in @interface.GetMethods().Where(m => !m.IsSpecialName))
            {
                var emitter = MethodEmitterFactory.CreateMethodEmitterFor(signature, Type, interceptorField);
                emitter.ImplemenMethod();
            }
        }

        /// <inheritdoc />
        public Type ToType()
        {
            try
            {
                var type = Type.CreateType();
                if (type == null)
                {
                    throw new Exception($"Unable to creat a proxy for \"{Type.Name}\"");
                }
                return type;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to creat a proxy for \"{Type.Name}\"", e);
            }
        }

        #endregion
    }
}