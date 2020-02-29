namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
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
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Equal(42, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of a non-async method")]
        public void ApplyReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Equal(value, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of an async method")]
        public void ApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<int>>(invocation.ReturnValue);
            Assert.Equal(42, ((Task<int>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Apply an arranged return value (task of value type) to an invocation of an async method")]
        public void ApplyAsyncValueTypeTaskReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<int>>(signature, Task.FromResult(42));

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<int>>(invocation.ReturnValue);
            Assert.Equal(42, ((Task<int>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of an async method")]
        public void ApplyAsyncReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<object?>>(invocation.ReturnValue);
            Assert.Equal(value, ((Task<object?>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void ApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<object?>>(signature, Task.FromResult<object?>(value));

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<object?>>(invocation.ReturnValue);
            Assert.Equal(value, ((Task<object?>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of a property getter")]
        public void ApplyValueTypeGetterReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<int>(getter, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Equal(42, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (reference type) to an invocation of a property getter")]
        public void ApplyReferenceTypeGetterReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<object?>(getter, value);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Equal(value, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that a return value arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Null(invocation.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked method signature matches")]
        public void CanApplyWithMatchingSignatureReturnsTrue()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.True(canApply);
            Assert.Null(invocation.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method signature does not match")]
        public void CanApplyWithNonMatchingSignatureReturnsFalse()
        {
            // Given
            var valueTypeSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.Null(invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of a non-async method")]
        public void TryApplyValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.Equal(42, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of a non-async method")]
        public void TryApplyReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.Equal(value, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of an async method")]
        public void TryApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<int>>(invocation.ReturnValue);
            Assert.Equal(42, ((Task<int>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (task of value type) to an invocation of an async method")]
        public void TryApplyAsyncValueTypeTaskReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<int>>(signature, Task.FromResult(42));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<int>>(invocation.ReturnValue);
            Assert.Equal(42, ((Task<int>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of an async method")]
        public void TryApplyAsyncReferenceTypeReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<object?>(signature, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<object?>>(invocation.ReturnValue);
            Assert.Equal(value, ((Task<object?>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void TryApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<object?>>(signature, Task.FromResult<object?>(value));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<object?>>(invocation.ReturnValue);
            Assert.Equal(value, ((Task<object?>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of a property getter")]
        public void TryApplyValueTypeGetterReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<int>(getter, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.Equal(42, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (reference type) to an invocation of a property getter")]
        public void TryApplyReferenceTypeGetterReturnValueArrangement()
        {
            // Given
            var value = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<object?>(getter, value);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.Equal(value, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
        public void EnsureTryApplyIsFalseForNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.Null(invocation.ReturnValue);
        }
    }
}