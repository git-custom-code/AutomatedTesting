namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using CustomCode.AutomatedTesting.Mocks.Interception.Properties;
    using Interception;
    using Interception.Async;
    using Interception.ReturnValue;
    using System;
    using System.Threading.Tasks;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="ReturnValueArrangement{T}"/> type.
    /// </summary>
    public sealed class ReturnValueArrangementTests
    {
        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of a non-async method")]
        public void ApplyValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(signature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, feature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of a non-async method")]
        public void ApplyReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooFuncReferenceTypeParameterless<object>)
                .GetMethod(nameof(IFooFuncReferenceTypeParameterless<object>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<object?>();
            var invocation = new Invocation(signature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var feature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, feature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of an async method")]
        public void ApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooGenericTaskValueTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<int>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<int>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<int>>>();
            Assert.IsAssignableFrom<Task<int>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (task of value type) to an invocation of an async method")]
        public void ApplyAsyncValueTypeTaskReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooGenericTaskValueTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<int>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<Task<int>>(signature, Task.FromResult(42));

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<int>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<int>>>();
            Assert.IsAssignableFrom<Task<int>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of an async method")]
        public void ApplyAsyncReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooGenericTaskReferenceTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<object?>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<object?>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<object?>>>();
            Assert.IsAssignableFrom<Task<object>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void ApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooGenericTaskReferenceTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<object?>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<Task<object?>>(signature, Task.FromResult<object?>(value));

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<object?>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<object?>>>();
            Assert.IsAssignableFrom<Task<object>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of a property getter")]
        public void ApplyValueTypeGetterReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();

            var propertyFeature = new PropertyInvocation(signature);
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(getter, propertyFeature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(getter, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, feature.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of a property getter")]
        public void ApplyReferenceTypeGetterReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();

            var propertyFeature = new PropertyInvocation(signature);
            var returnValueFeature = new ReturnValueInvocation<object?>();
            var invocation = new Invocation(getter, propertyFeature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<object?>(getter, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var feature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, feature.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that a return value arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooFuncReferenceTypeParameterless<object>)
                .GetMethod(nameof(IFooFuncReferenceTypeParameterless<object>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(referenceTypeSignature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(default, feature.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked method signature matches")]
        public void CanApplyWithMatchingSignatureReturnsTrue()
        {
            // Given
            var signature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(signature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.True(canApply);
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(default, feature.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method signature does not match")]
        public void CanApplyWithNonMatchingSignatureReturnsFalse()
        {
            // Given
            var valueTypeSignature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooFuncReferenceTypeParameterless<object>)
                .GetMethod(nameof(IFooFuncReferenceTypeParameterless<object>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(referenceTypeSignature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(default, feature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of a non-async method")]
        public void TryApplyValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(signature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, feature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of a non-async method")]
        public void TryApplyReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooFuncReferenceTypeParameterless<object>)
                .GetMethod(nameof(IFooFuncReferenceTypeParameterless<object>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<object?>();
            var invocation = new Invocation(signature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var feature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, feature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of an async method")]
        public void TryApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooGenericTaskValueTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<int>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);

            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<int>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<int>>>();
            Assert.IsAssignableFrom<Task<int>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (task of value type) to an invocation of an async method")]
        public void TryApplyAsyncValueTypeTaskReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooGenericTaskValueTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskValueTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<int>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<Task<int>>(signature, Task.FromResult(42));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);

            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<int>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<int>>>();
            Assert.IsAssignableFrom<Task<int>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of an async method")]
        public void TryApplyAsyncReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooGenericTaskReferenceTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<object?>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);

            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<object?>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<object?>>>();
            Assert.IsAssignableFrom<Task<object?>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void TryApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooGenericTaskReferenceTypeParameterless)
                .GetMethod(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var asyncFeature = new AsyncGenericTaskInvocation<object?>();
            var invocation = new Invocation(signature, asyncFeature);
            var arrangment = new ReturnValueArrangement<Task<object?>>(signature, Task.FromResult<object?>(value));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);

            Assert.True(invocation.HasFeature<IAsyncInvocation<Task<object?>>>());
            var feature = invocation.GetFeature<IAsyncInvocation<Task<object?>>>();
            Assert.IsAssignableFrom<Task<object?>>(feature.AsyncReturnValue);

            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var returnValueFeature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, returnValueFeature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of a property getter")]
        public void TryApplyValueTypeGetterReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();

            var propertyFeature = new PropertyInvocation(signature);
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(getter, propertyFeature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(getter, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(42, feature.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of a property getter")]
        public void TryApplyReferenceTypeGetterReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();

            var propertyFeature = new PropertyInvocation(signature);
            var returnValueFeature = new ReturnValueInvocation<object?>();
            var invocation = new Invocation(getter, propertyFeature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<object?>(getter, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.True(invocation.HasFeature<IReturnValue<object?>>());
            var feature = invocation.GetFeature<IReturnValue<object?>>();
            Assert.Equal(value, feature.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
        public void EnsureTryApplyIsFalseForNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFooFuncValueTypeParameterless<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooFuncReferenceTypeParameterless<object>)
                .GetMethod(nameof(IFooFuncReferenceTypeParameterless<object>.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var returnValueFeature = new ReturnValueInvocation<int>();
            var invocation = new Invocation(referenceTypeSignature, returnValueFeature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.True(invocation.HasFeature<IReturnValue<int>>());
            var feature = invocation.GetFeature<IReturnValue<int>>();
            Assert.Equal(default, feature.ReturnValue);
        }
    }
}