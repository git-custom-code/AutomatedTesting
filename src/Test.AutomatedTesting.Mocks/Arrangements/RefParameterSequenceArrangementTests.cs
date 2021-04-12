namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using ExceptionHandling;
    using Interception;
    using Interception.Parameters;
    using System.Collections.Generic;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="RefParameterSequenceArrangement{T}"/> type.
    /// </summary>
    public sealed class RefParameterSequenceArrangementTests
    {
        [Fact(DisplayName = "Apply a sequence of ref parameter values (value type) to an invocation of a (void) method")]
        public void ApplySequenceOfValueTypeRefParametersArrangement()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            arrangment.ApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.Equal(13, first);
            Assert.Equal(42, second);
            Assert.Equal(65, third);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of ref parameter values (value type) to an invocation of a method")]
        public void ApplySequenceOfValueTypeRefParametersArrangementFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            arrangment.ApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.Equal(13, first);
            Assert.Equal(42, second);
            Assert.Equal(65, third);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of ref parameter values (reference type) to an invocation of a (void) method")]
        public void ApplySequenceOfReferenceTypeRefParametersArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var type = typeof(IFooActionReferenceTypeParameterRef<object>);
            var methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object?>(
                signature, "first", new List<object?> { value1, value2, value3 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            arrangment.ApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.Equal(value1, first);
            Assert.Equal(value2, second);
            Assert.Equal(value3, third);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of ref parameter values (reference type) to an invocation of a method")]
        public void ApplySequenceOfReferenceTypeRefParametersArrangementFunc()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var type = typeof(IFooFuncReferenceTypeParameterRef<object>);
            var methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object?>(
                signature, "first", new List<object?> { value1, value2, value3 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            arrangment.ApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            arrangment.ApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.Equal(value1, first);
            Assert.Equal(value2, second);
            Assert.Equal(value3, third);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Ensure that a ref parameter sequence arrangement is only applied if the invoked (void) method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            type = typeof(IFooActionReferenceTypeParameterRef<object>);
            methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

            var refParameterFeature = new ParameterRef(valueTypeSignature, new object[] { 9 });
            var invocation = new Invocation(valueTypeSignature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object>(
                referenceTypeSignature, "first", new List<object> { new object() });

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that a ref parameter sequence arrangement is only applied if the invoked (void) method parameter matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocation()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that a ref parameter sequence arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocationFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            type = typeof(IFooFuncReferenceTypeParameterRef<object>);
            methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

            var refParameterFeature = new ParameterRef(valueTypeSignature, new object[] { 9 });
            var invocation = new Invocation(valueTypeSignature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object>(
                referenceTypeSignature, "first", new List<object> { new object() });

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that a ref parameter sequence arrangement is only applied if the invoked method parameter matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocationFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked (void) method signature matches")]
        public void CanApplySequenceWithMatchingSignatureReturnsTrue()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 42 });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.True(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked method signature matches")]
        public void CanApplySequenceWithMatchingSignatureReturnsTrueFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 42 });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.True(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method signature does not match")]
        public void CanApplySequenceWithNonMatchingSignatureReturnsFalse()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            type = typeof(IFooActionReferenceTypeParameterRef<object>);
            methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

            var refParameterFeature = new ParameterRef(valueTypeSignature, new object[] { 9 });
            var invocation = new Invocation(valueTypeSignature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object>(
                referenceTypeSignature, "first", new List<object> { new object() });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method parameter does not match")]
        public void CanApplySequenceWithNonMatchingParameterReturnsFalse()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 42 });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method signature does not match")]
        public void CanApplySequenceWithNonMatchingSignatureReturnsFalseFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            type = typeof(IFooFuncReferenceTypeParameterRef<object>);
            methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

            var refParameterFeature = new ParameterRef(valueTypeSignature, new object[] { 9 });
            var invocation = new Invocation(valueTypeSignature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object>(
                referenceTypeSignature, "first", new List<object> { new object() });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method parameter does not match")]
        public void CanApplySequenceWithNonMatchingParameterReturnsFalseFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 42 });

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Try to apply a sequence of ref parameter values (value type) to an invocation of a (void) method")]
        public void TryApplySequenceOfValueTypeRefParametersArrangement()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            var wasFirstApplied = arrangment.TryApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            var wasSecondApplied = arrangment.TryApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            var wasThirdApplied = arrangment.TryApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            var wasFourthApplied = arrangment.TryApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.True(wasFirstApplied);
            Assert.Equal(13, first);
            Assert.True(wasSecondApplied);
            Assert.Equal(42, second);
            Assert.True(wasThirdApplied);
            Assert.Equal(65, third);
            Assert.True(wasFourthApplied);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Try to apply a sequence of ref parameter values (value type) to an invocation of a method")]
        public void TryApplySequenceOfValueTypeRefParametersArrangementFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            var wasFirstApplied = arrangment.TryApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            var wasSecondApplied = arrangment.TryApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            var wasThirdApplied = arrangment.TryApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            var wasFourthApplied = arrangment.TryApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.True(wasFirstApplied);
            Assert.Equal(13, first);
            Assert.True(wasSecondApplied);
            Assert.Equal(42, second);
            Assert.True(wasThirdApplied);
            Assert.Equal(65, third);
            Assert.True(wasFourthApplied);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Try to apply a sequence of ref parameter values (reference type) to an invocation of a (void) method")]
        public void TryApplySequenceOfReferenceTypeRefParametersArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var type = typeof(IFooActionReferenceTypeParameterRef<object>);
            var methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object?>(
               signature, "first", new List<object?> { value1, value2, value3 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            var wasFirstApplied = arrangment.TryApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            var wasSecondApplied = arrangment.TryApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            var wasThirdApplied = arrangment.TryApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            var wasFourthApplied = arrangment.TryApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.True(wasFirstApplied);
            Assert.Equal(value1, first);
            Assert.True(wasSecondApplied);
            Assert.Equal(value2, second);
            Assert.True(wasThirdApplied);
            Assert.Equal(value3, third);
            Assert.True(wasFourthApplied);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Try to apply a sequence of ref parameter values (reference type) to an invocation of a method")]
        public void TryApplySequenceOfReferenceTypeRefParametersArrangementFunc()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var type = typeof(IFooFuncReferenceTypeParameterRef<object>);
            var methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<object?>(
                signature, "first", new List<object?> { value1, value2, value3 });

            // When
            var feature = invocation.GetFeature<IParameterRef>();
            var wasFirstApplied = arrangment.TryApplyTo(invocation);
            var first = feature.RefParameterCollection.First().Value;
            var wasSecondApplied = arrangment.TryApplyTo(invocation);
            var second = feature.RefParameterCollection.First().Value;
            var wasThirdApplied = arrangment.TryApplyTo(invocation);
            var third = feature.RefParameterCollection.First().Value;
            var wasFourthApplied = arrangment.TryApplyTo(invocation);
            var fourth = feature.RefParameterCollection.First().Value;

            // Then
            Assert.True(wasFirstApplied);
            Assert.Equal(value1, first);
            Assert.True(wasSecondApplied);
            Assert.Equal(value2, second);
            Assert.True(wasThirdApplied);
            Assert.Equal(value3, third);
            Assert.True(wasFourthApplied);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked (void) method signature does not match")]
        public void EnsureTryApplySequenceIsFalseForNonMatchingInvocation()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterRef<int>);
            var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal("first", parameter.Name);
            Assert.Equal(typeof(int), parameter.Type);
            Assert.Equal(9, parameter.Value);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
        public void EnsureTryApplySequenceIsFalseForNonMatchingInvocationFunc()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterRef<int>);
            var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var refParameterFeature = new ParameterRef(signature, new object[] { 9 });
            var invocation = new Invocation(signature, refParameterFeature);
            var arrangment = new RefParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.True(invocation.HasFeature<IParameterRef>());
            var feature = invocation.GetFeature<IParameterRef>();
            Assert.Single(feature.RefParameterCollection);
            var parameter = feature.RefParameterCollection.Single();
            Assert.Equal("first", parameter.Name);
            Assert.Equal(typeof(int), parameter.Type);
            Assert.Equal(9, parameter.Value);
        }
    }
}