namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception.Parameters;
    using Interception.Properties;
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
        /// Gets the cached signature of the <see cref="PropertyInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreatePropertyInvocation { get; }
            = new Lazy<ConstructorInfo>(InitializeCreatePropertyInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits a local <see cref="PropertyInvocation"/> feature variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="propertyFeatureVariable"> The emitted local <see cref="PropertyInvocation"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyInvocation propertyFeature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalPropertyFeatureVariable(
            this ILGenerator body, out LocalBuilder propertyFeatureVariable)
        {
            propertyFeatureVariable = body.DeclareLocal(typeof(PropertyInvocation));
        }

        /// <summary>
        /// Emits code to create a new <see cref="PropertyInvocation"/> instance for the given feature.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="propertyFeatureVariable"> The emitted local <see cref="PropertyInvocation"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     propertyFeature = new PropertyInvocation(propertySignature);
        /// ]]>
        /// </remarks>
        public static void EmitNewPropertyFeature(
            this ILGenerator body,
            LocalBuilder propertySignatureVariable,
            LocalBuilder propertyFeatureVariable)
        {
            // propertySignature,
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);

            // propertyFeature = new PropertyInvocation(...)
            body.Emit(OpCodes.Newobj, CreatePropertyInvocation.Value);
            body.Emit(OpCodes.Stloc, propertyFeatureVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreatePropertyInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="PropertyInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreatePropertyInvocation()
        {
            var type = typeof(PropertyInvocation);
            var constructor = type.GetConstructor(new[] { typeof(PropertyInfo) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}