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
        /// Gets the cached signature of the <see cref="SetterInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateSetterInvocation { get; } = new Lazy<ConstructorInfo>(InitializeCreateSetterInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     SetterInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="SetterInvocation"/> variable. </param>
        public static void EmitLocalSetterInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(SetterInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new SetterInvocation(propertySignature, value);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        /// <param name="propertyType"> The type of the dynamic property. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="SetterInvocation"/> variable. </param>
        public static void EmitNewSetterInvocation(
            this ILGenerator body,
            Type propertyType,
            LocalBuilder propertySignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);
            body.Emit(OpCodes.Ldarg_1);
            if (propertyType.IsValueType)
            {
                body.Emit(OpCodes.Box, propertyType);
            }
            body.Emit(OpCodes.Newobj, CreateSetterInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }


        /// <summary>
        /// Initialization logic for the <see cref="CreateSetterInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="SetterInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateSetterInvocation()
        {
            var type = typeof(SetterInvocation);
            var constructor = @type.GetConstructor(new[] { typeof(PropertyInfo), typeof(object) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}