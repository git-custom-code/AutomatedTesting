namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;

    /// <summary>
    /// Default implementation of the <see cref="IAssemblyEmitter"/> interface.
    /// </summary>
    public sealed class AssemblyEmitter : IAssemblyEmitter
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="AssemblyEmitter"/> type.
        /// </summary>
        /// <param name="typeEmitterFactory">
        /// A factory that can be used to create a new <see cref="ITypeEmitter"/> from an existing <see cref="TypeBuilder"/>.
        /// </param>
        /// <param name="typeDecoratorEmitterFactory">
        /// A factory that can be used to create a new <see cref="ITypeDecoratorEmitter"/>
        /// from an existing <see cref="TypeBuilder"/>.
        /// </param>
        public AssemblyEmitter(
            Func<TypeBuilder, ITypeEmitter> typeEmitterFactory,
            Func<TypeBuilder, ITypeDecoratorEmitter> typeDecoratorEmitterFactory)
        {
            Module = new Lazy<ModuleBuilder>(
                CreateDynamicAssembly,
                LazyThreadSafetyMode.ExecutionAndPublication);
            CreateTypeEmitter = typeEmitterFactory ?? throw new ArgumentNullException(nameof(typeEmitterFactory));
            CreateTypeDecoratorEmitter = typeDecoratorEmitterFactory ?? throw new ArgumentNullException(nameof(typeDecoratorEmitterFactory));
        }

        /// <summary>
        /// Gets a singleton <see cref="ModuleBuilder"/> that can be used to dynamically create types.
        /// </summary>
        private Lazy<ModuleBuilder> Module { get; }

        /// <summary>
        /// Gets a factory that can be used to create a new <see cref="ITypeEmitter"/> from an existing <see cref="TypeBuilder"/>.
        /// </summary>
        private Func<TypeBuilder, ITypeEmitter> CreateTypeEmitter { get; }

        /// <summary>
        /// Gets a factory that can be used to create a new <see cref="ITypeDecoratorEmitter"/>
        /// from an existing <see cref="TypeBuilder"/>.
        /// </summary>
        private Func<TypeBuilder, ITypeDecoratorEmitter> CreateTypeDecoratorEmitter { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Creates a new dynamic in-memory assembly (named "DynamicMockAssembly") along with a single module.
        /// </summary>
        /// <returns> A <see cref="ModuleBuilder"/> that can be used to dynamically create types. </returns>
        private ModuleBuilder CreateDynamicAssembly()
        {
            var name = new AssemblyName("DynamicMockAssembly");
            var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
            var module = assembly.DefineDynamicModule("DynamicMockModule");
            return module;
        }

        /// <inheritdoc cref="IAssemblyEmitter" />
        public ITypeEmitter EmitType(string typeFullName)
        {
            var builder = Module.Value.DefineType(
                typeFullName,
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);
            var emitter = CreateTypeEmitter(builder);
            return emitter;
        }

        /// <inheritdoc cref="IAssemblyEmitter" />
        public ITypeDecoratorEmitter EmitDecoratorType(string typeFullName)
        {
            var builder = Module.Value.DefineType(
                typeFullName,
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);
            var emitter = CreateTypeDecoratorEmitter(builder);
            return emitter;
        }

        #endregion
    }
}