namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
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
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Type>(signature, type);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.Equal(type, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Apply an arranged return value (value type) to an invocation of an async method")]
        public void ApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Type>(signature, type);

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<Type>>(invocation.ReturnValue);
            Assert.Equal(type, ((Task<Type>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void ApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<Type>>(signature, Task.FromResult(type));

            // When
            arrangment.ApplyTo(invocation);

            // Then
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<Type>>(invocation.ReturnValue);
            Assert.Equal(type, ((Task<Type>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Ensure that a return value arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
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
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
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
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Type>(signature, type);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.Equal(type, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (value type) to an invocation of an async method")]
        public void TryApplyAsyncValueTypeReturnValueArrangement()
        {
            // Given
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Type>(signature, type);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<Type>>(invocation.ReturnValue);
            Assert.Equal(type, ((Task<Type>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Try to apply an arranged return value (task of reference type) to an invocation of an async method")]
        public void TryApplyAsyncReferenceTypeTaskReturnValueArrangement()
        {
            // Given
            var type = typeof(object);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<Task<Type>>(signature, Task.FromResult(type));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.True(wasApplied);
            Assert.NotNull(invocation.ReturnValue);
            Assert.IsAssignableFrom<Task<Type>>(invocation.ReturnValue);
            Assert.Equal(type, ((Task<Type>)invocation.ReturnValue).Result);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
        public void EnsureTryAppliyIsFalseForNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueArrangement<int>(valueTypeSignature, 42);

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.Null(invocation.ReturnValue);
        }

        #region Domain

        public interface IFoo
        {
            int ValueType();

            Type ReferenceType();

            Task<int> ValueTypeAsync();

            Task<Type> ReferenceTypeAsync();
        }

        #endregion
    }
}