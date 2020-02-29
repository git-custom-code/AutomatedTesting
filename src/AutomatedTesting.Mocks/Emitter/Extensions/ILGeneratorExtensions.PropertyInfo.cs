namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
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
        /// Gets the cached signature of the <see cref="Type.GetProperty(string)"/> method.
        /// </summary>
        private static Lazy<MethodInfo> GetProperty { get; } = new Lazy<MethodInfo>(InitializeGetProperty, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyInfo propertySignature;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        public static void EmitLocalPropertySignatureVariable(this ILGenerator body, out LocalBuilder propertySignatureVariable)
        {
            propertySignatureVariable = body.DeclareLocal(typeof(PropertyInfo));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     propertySignature = typeof(Interface).GetProperty(nameof(Property));
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="signature"> The dynamic property's signature. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        public static void EmitGetPropertySignature(
            this ILGenerator body,
            PropertyInfo signature,
            LocalBuilder propertySignatureVariable)
        {
            body.Emit(OpCodes.Nop);
#pragma warning disable CS8604 // Possible null reference argument.
            body.Emit(OpCodes.Ldtoken, signature.DeclaringType);
#pragma warning restore CS8604
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, signature.Name);
            body.Emit(OpCodes.Call, GetProperty.Value);
            body.Emit(OpCodes.Stloc, propertySignatureVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetProperty"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetProperty(string)"/> method. </returns>
        private static MethodInfo InitializeGetProperty()
        {
            var @type = typeof(Type);
            var propertyName = nameof(Type.GetProperty);
            var getProperty = @type.GetMethod(propertyName, new[] { typeof(string) });
            return getProperty ?? throw new PropertyInfoException(@type, propertyName);
        }

        #endregion
    }
}