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
        public AssemblyEmitter()
        {
            Module = new Lazy<ModuleBuilder>(
                CreateDynamicAssembly,
                LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        /// Gets a singleton <see cref="ModuleBuilder"/> that can be used to dynamically create types.
        /// </summary>
        private Lazy<ModuleBuilder> Module { get; }

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

        /// <inheritdoc />
        public Type EmitType(string typeFullName)
        {
            var builder = Module.Value.DefineType(
                typeFullName,
                TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);
            return builder.CreateType();
        }

        #endregion
    }
}