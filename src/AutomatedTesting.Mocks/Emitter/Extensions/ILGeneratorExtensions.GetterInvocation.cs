namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="GetterInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateGetterInvocation { get; } = new Lazy<ConstructorInfo>(InitializeCreateGetterInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     GetterInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="GetterInvocation"/> variable. </param>
        public static void EmitLocalGetterInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(GetterInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new GetterInvocation(propertySignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="GetterInvocation"/> variable. </param>
        public static void EmitNewGetterInvocation(
            this ILGenerator body,
            LocalBuilder propertySignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, CreateGetterInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateGetterInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="GetterInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateGetterInvocation()
        {
            var @type = typeof(GetterInvocation);
            var constructor = @type.GetConstructor(new[] { typeof(PropertyInfo) });
            return constructor ?? throw new ConstructorInfoException(@type);
        }

        #endregion
    }
}