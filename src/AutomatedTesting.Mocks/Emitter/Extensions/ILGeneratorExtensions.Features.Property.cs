namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
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

        /// <summary>
        /// Gets the cached signature of the <see cref="PropertySetterValue"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreatePropertySetterValue { get; }
            = new Lazy<ConstructorInfo>(InitializeCreatePropertySetterValue, true);

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
        /// Emits a local <see cref="PropertySetterValue"/> feature variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic property setter. </param>
        /// <param name="propertySetterValueFeatureVariable">
        /// The emitted local <see cref="PropertySetterValue"/> variable.
        /// </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertySetterValue propertySetterValueFeature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalPropertySetterValueFeatureVariable(
            this ILGenerator body, out LocalBuilder propertySetterValueFeatureVariable)
        {
            propertySetterValueFeatureVariable = body.DeclareLocal(typeof(PropertySetterValue));
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
        /// Emits code to create a new <see cref="PropertySetterValue"/> instance for the given feature.
        /// </summary>
        /// <param name="body"> The body of the dynamic property setter. </param>
        /// <param name="signature"> The property's signature. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="propertySetterValueFeatureVariable"> The emitted local <see cref="PropertySetterValue"/> variable. </param>
        /// <param name="valueParameterIndex"> The index of the setter's value parameter. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     propertySetterValueFeature = new PropertySetterValue(propertySignature, value);
        /// ]]>
        /// </remarks>
        public static void EmitNewPropertySetterValueFeature(
            this ILGenerator body,
            PropertyInfo signature,
            LocalBuilder propertySignatureVariable,
            LocalBuilder propertySetterValueFeatureVariable,
            int valueParameterIndex = 1)
        {
            // propertySignature,
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);

            // value
            body.Emit(OpCodes.Ldarg, valueParameterIndex);
            if (signature.PropertyType.IsValueType)
            {
                body.Emit(OpCodes.Box, signature.PropertyType);
            }

            // propertySetterValueFeature = new PropertySetterValue(...)
            body.Emit(OpCodes.Newobj, CreatePropertySetterValue.Value);
            body.Emit(OpCodes.Stloc, propertySetterValueFeatureVariable.LocalIndex);
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

        /// <summary>
        /// Initialization logic for the <see cref="CreatePropertySetterValue"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="PropertySetterValue"/> constructor. </returns>
        private static ConstructorInfo InitializeCreatePropertySetterValue()
        {
            var type = typeof(PropertySetterValue);
            var constructor = type.GetConstructor(new[] { typeof(PropertyInfo), typeof(object) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}