namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
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
        public TypeEmitter(TypeBuilder typeBuilder)
        {
            Type = typeBuilder;
        }

        /// <summary>
        /// Gets the internal <see cref="TypeBuilder"/> that is used to dynamically emit logic.
        /// </summary>
        private TypeBuilder Type { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void ImplementInterface(Type @interface)
        {
            if (!@interface.IsInterface)
            {
                throw new ArgumentException($"Invalid non-interface type '{@interface.FullName}'");
            }

            Type.AddInterfaceImplementation(@interface);
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