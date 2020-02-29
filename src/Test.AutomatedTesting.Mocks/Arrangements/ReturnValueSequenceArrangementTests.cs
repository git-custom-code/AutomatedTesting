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
    /// Automated tests for the <see cref="ReturnValueSequenceArrangement{T}"/> type.
    /// </summary>
    public sealed class ReturnValueSequenceArrangementTests
    {
        [Fact(DisplayName = "Apply a sequence of arranged return values (value type) to an invocation of a non-async method")]
        public void ApplySequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(signature, new List<int>(new[] { 13, 42, 65 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(13, first);
            Assert.Equal(42, second);
            Assert.Equal(65, third);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (reference type) to an invocation of a non-async method")]
        public void ApplySequenceOfReferenceTypeReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var signature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(signature, new List<object?>(new[] { value1, value2, value3 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(value1, first);
            Assert.Equal(value2, second);
            Assert.Equal(value3, third);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (value type) to an invocation of an async method")]
        public void ApplyAsyncSequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(signature, new List<int>(new[] { 13, 42, 65 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<int>>(first);
            Assert.Equal(13, ((Task<int>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<int>>(second);
            Assert.Equal(42, ((Task<int>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<int>>(third);
            Assert.Equal(65, ((Task<int>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<int>>(fourth);
            Assert.Equal(65, ((Task<int>)fourth).Result);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (task of value type) to an invocation of an async method")]
        public void ApplyAsyncSequenceOfValueTypeTaskReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<int>>(signature, new List<Task<int>>(new[]
                {
                    Task.FromResult(13),
                    Task.FromResult(42),
                    Task.FromResult(65)
                }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<int>>(first);
            Assert.Equal(13, ((Task<int>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<int>>(second);
            Assert.Equal(42, ((Task<int>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<int>>(third);
            Assert.Equal(65, ((Task<int>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<int>>(fourth);
            Assert.Equal(65, ((Task<int>)fourth).Result);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (reference type) to an invocation of an async method")]
        public void ApplyAsyncSequnceOfReferenceTypeReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(signature, new List<object?>(new[] { value1, value2, value3 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<object?>>(first);
            Assert.Equal(value1, ((Task<object?>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<object?>>(second);
            Assert.Equal(value2, ((Task<object?>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<object?>>(third);
            Assert.Equal(value3, ((Task<object?>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<object?>>(fourth);
            Assert.Equal(value3, ((Task<object?>)fourth).Result);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (task of reference type) to an invocation of an async method")]
        public void ApplyAsyncSequenceOfReferenceTypeTaskReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<object?>>(signature, new List<Task<object?>>(new[]
                {
                    Task.FromResult<object?>(value1),
                    Task.FromResult<object?>(value2),
                    Task.FromResult<object?>(value3)
                }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<object?>>(first);
            Assert.Equal(value1, ((Task<object?>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<object?>>(second);
            Assert.Equal(value2, ((Task<object?>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<object?>>(third);
            Assert.Equal(value3, ((Task<object?>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<object?>>(fourth);
            Assert.Equal(value3, ((Task<object?>)fourth).Result);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (value type) to an invocation of a property getter")]
        public void ApplySequenceOfValueTypeReturnValuesGetterArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(getter, new List<int>(new[] { 13, 42, 65 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(13, first);
            Assert.Equal(42, second);
            Assert.Equal(65, third);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (value type) to an invocation of a property getter")]
        public void ApplySequenceOfValueTypeReturnValuesGetterSetterArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.GetterSetter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(getter, new List<int>(new[] { 13, 42, 65 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(13, first);
            Assert.Equal(42, second);
            Assert.Equal(65, third);
            Assert.Equal(65, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (reference type) to an invocation of a property getter")]
        public void ApplySequenceOfReferenceTypeReturnValuesGetterArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(getter, new List<object?>(new[] { value1, value2, value3 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(value1, first);
            Assert.Equal(value2, second);
            Assert.Equal(value3, third);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (reference type) to an invocation of a property getter")]
        public void ApplySequenceOfReferenceTypeReturnValuesGetterSetterArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var value3 = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.GetterSetter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(getter, new List<object?>(new[] { value1, value2, value3 }));

            // When
            arrangment.ApplyTo(invocation);
            var first = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var second = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var third = invocation.ReturnValue;
            arrangment.ApplyTo(invocation);
            var fourth = invocation.ReturnValue;

            // Then
            Assert.Equal(value1, first);
            Assert.Equal(value2, second);
            Assert.Equal(value3, third);
            Assert.Equal(value3, fourth);
        }

        [Fact(DisplayName = "Ensure that a sequence of return values arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueSequenceArrangement<int>(valueTypeSignature, new List<int>(new[] { 42 }));

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
            var arrangment = new ReturnValueSequenceArrangement<int>(signature, new List<int>(new[] { 42 }));

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
            var arrangment = new ReturnValueSequenceArrangement<int>(valueTypeSignature, new List<int>(new[] { 42 }));

            // When
            var canApply = arrangment.CanApplyTo(invocation);

            // Then
            Assert.False(canApply);
            Assert.Null(invocation.ReturnValue);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (value type) to an invocation of a non-async method")]
        public void TryApplySequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(signature, new List<int>(new[] { 13, 42 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(13, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(42, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(42, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (reference type) to an invocation of a non-async method")]
        public void TryApplySequenceOfReferenceTypeReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var signature = typeof(IFooWithReferenceTypeFunc)
                .GetMethod(nameof(IFooWithReferenceTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(signature, new List<object?>(new[] { value1, value2 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(value1, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(value2, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(value2, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (value type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(signature, new List<int>(new[] { 13, 42 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<int>>(first);
            Assert.Equal(13, ((Task<int>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<int>>(second);
            Assert.Equal(42, ((Task<int>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<int>>(third);
            Assert.Equal(42, ((Task<int>)third).Result);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (task of value type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfValueTypeTaskReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFooWithAsyncValueTypeFunc)
                .GetMethod(nameof(IFooWithAsyncValueTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<int>>(signature, new List<Task<int>>(new[]
                {
                    Task.FromResult(13),
                    Task.FromResult(42)
                }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<int>>(first);
            Assert.Equal(13, ((Task<int>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<int>>(second);
            Assert.Equal(42, ((Task<int>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<int>>(third);
            Assert.Equal(42, ((Task<int>)third).Result);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (reference type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfReferenceTypeReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(signature, new List<object?>(new[] { value1, value2 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<object?>>(first);
            Assert.Equal(value1, ((Task<object?>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<object?>>(second);
            Assert.Equal(value2, ((Task<object?>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<object?>>(third);
            Assert.Equal(value2, ((Task<object?>)third).Result);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (task of reference type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfReferenceTypeTaskReturnValuesArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var signature = typeof(IFooWithAsyncReferenceTypeFunc)
                .GetMethod(nameof(IFooWithAsyncReferenceTypeFunc.MethodWithoutParameterAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<object?>>(signature, new List<Task<object?>>(new[]
                {
                    Task.FromResult<object?>(value1),
                    Task.FromResult<object?>(value2)
                }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.NotNull(first);
            Assert.IsAssignableFrom<Task<object?>>(first);
            Assert.Equal(value1, ((Task<object?>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<object?>>(second);
            Assert.Equal(value2, ((Task<object?>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<object?>>(third);
            Assert.Equal(value2, ((Task<object?>)third).Result);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (value type) to an invocation of a property getter")]
        public void TryApplySequenceOfValueTypeReturnValuesGetterArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(getter, new List<int>(new[] { 13, 42 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(13, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(42, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(42, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (value type) to an invocation of a property getter")]
        public void TryApplySequenceOfValueTypeReturnValuesGetterSetterArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeProperties)
                .GetProperty(nameof(IFooWithValueTypeProperties.GetterSetter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<int>(getter, new List<int>(new[] { 13, 42 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(13, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(42, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(42, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (reference type) to an invocation of a property getter")]
        public void TryApplySequenceOfReferenceTypeReturnValuesGetterArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.Getter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(getter, new List<object?>(new[] { value1, value2 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(value1, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(value2, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(value2, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (reference type) to an invocation of a property getter")]
        public void TryApplySequenceOfReferenceTypeReturnValuesGetterSetterArrangement()
        {
            // Given
            var value1 = new object();
            var value2 = new object();
            var signature = typeof(IFooWithReferenceTypeProperties)
                .GetProperty(nameof(IFooWithReferenceTypeProperties.GetterSetter)) ?? throw new InvalidOperationException();
            var getter = signature.GetGetMethod() ?? throw new InvalidOperationException();
            var invocation = new GetterInvocation(signature);
            var arrangment = new ReturnValueSequenceArrangement<object?>(getter, new List<object?>(new[] { value1, value2 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(value1, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(value2, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(value2, third);
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
            var arrangment = new ReturnValueSequenceArrangement<int>(valueTypeSignature, new List<int>(new[] { 42 }));

            // When
            var wasApplied = arrangment.TryApplyTo(invocation);

            // Then
            Assert.False(wasApplied);
            Assert.Null(invocation.ReturnValue);
        }
    }
}