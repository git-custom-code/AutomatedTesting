namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
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
            var type1 = typeof(object);
            var type2 = typeof(string);
            var type3 = typeof(int);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Type>(signature, new List<Type>(new[] { type1, type2, type3 }));

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
            Assert.Equal(type1, first);
            Assert.Equal(type2, second);
            Assert.Equal(type3, third);
            Assert.Equal(type3, fourth);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (value type) to an invocation of an async method")]
        public void ApplyAsyncSequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var type1 = typeof(object);
            var type2 = typeof(string);
            var type3 = typeof(int);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Type>(signature, new List<Type>(new[] { type1, type2, type3 }));

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
            Assert.IsAssignableFrom<Task<Type>>(first);
            Assert.Equal(type1, ((Task<Type>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<Type>>(second);
            Assert.Equal(type2, ((Task<Type>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<Type>>(third);
            Assert.Equal(type3, ((Task<Type>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<Type>>(fourth);
            Assert.Equal(type3, ((Task<Type>)fourth).Result);
        }

        [Fact(DisplayName = "Apply a sequence of arranged return values (task of reference type) to an invocation of an async method")]
        public void ApplyAsyncSequenceOfReferenceTypeTaskReturnValuesArrangement()
        {
            // Given
            var type1 = typeof(object);
            var type2 = typeof(string);
            var type3 = typeof(int);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<Type>>(signature, new List<Task<Type>>(new[]
                {
                    Task.FromResult(type1),
                    Task.FromResult(type2),
                    Task.FromResult(type3)
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
            Assert.IsAssignableFrom<Task<Type>>(first);
            Assert.Equal(type1, ((Task<Type>)first).Result);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<Type>>(second);
            Assert.Equal(type2, ((Task<Type>)second).Result);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<Type>>(third);
            Assert.Equal(type3, ((Task<Type>)third).Result);
            Assert.NotNull(fourth);
            Assert.IsAssignableFrom<Task<Type>>(fourth);
            Assert.Equal(type3, ((Task<Type>)fourth).Result);
        }

        [Fact(DisplayName = "Ensure that a sequence of return values arrangement is only applied if the invoked method signature matches")]
        public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueSequenceArrangement<int>(valueTypeSignature, new List<int>(new[] { 42 } ));

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
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
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
            var type1 = typeof(object);
            var type2 = typeof(bool);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Type>(signature, new List<Type>(new[] { type1, type2 }));

            // When
            var wasAppliedFirst = arrangment.TryApplyTo(invocation);
            var first = invocation.ReturnValue;
            var wasAppliedSecond = arrangment.TryApplyTo(invocation);
            var second = invocation.ReturnValue;
            var wasAppliedThird = arrangment.TryApplyTo(invocation);
            var third = invocation.ReturnValue;

            // Then
            Assert.True(wasAppliedFirst);
            Assert.Equal(type1, first);
            Assert.True(wasAppliedSecond);
            Assert.Equal(type2, second);
            Assert.True(wasAppliedThird);
            Assert.Equal(type2, third);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (value type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfValueTypeReturnValuesArrangement()
        {
            // Given
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
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
            var type1 = typeof(object);
            var type2 = typeof(bool);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Type>(signature, new List<Type>(new[] { type1, type2 }));

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
            Assert.IsAssignableFrom<Task<Type>>(first);
            Assert.Equal(type1, ((Task<Type>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<Type>>(second);
            Assert.Equal(type2, ((Task<Type>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<Type>>(third);
            Assert.Equal(type2, ((Task<Type>)third).Result);
        }

        [Fact(DisplayName = "Try to apply a sequence of arranged return values (task of reference type) to an invocation of an async method")]
        public void TryApplyAsyncSequenceOfReferenceTypeTaskReturnValuesArrangement()
        {
            // Given
            var type1 = typeof(object);
            var type2 = typeof(bool);
            var signature = typeof(IFoo).GetMethod(nameof(IFoo.ValueTypeAsync)) ?? throw new InvalidOperationException();
            var invocation = new AsyncFuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueSequenceArrangement<Task<Type>>(signature, new List<Task<Type>>(new[]
                {
                    Task.FromResult(type1),
                    Task.FromResult(type2)
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
            Assert.IsAssignableFrom<Task<Type>>(first);
            Assert.Equal(type1, ((Task<Type>)first).Result);
            Assert.True(wasAppliedSecond);
            Assert.NotNull(second);
            Assert.IsAssignableFrom<Task<Type>>(second);
            Assert.Equal(type2, ((Task<Type>)second).Result);
            Assert.True(wasAppliedThird);
            Assert.NotNull(third);
            Assert.IsAssignableFrom<Task<Type>>(third);
            Assert.Equal(type2, ((Task<Type>)third).Result);
        }

        [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
        public void EnsureTryApplyIsFalseForNonMatchingInvocation()
        {
            // Given
            var valueTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ValueType)) ?? throw new InvalidOperationException();
            var referenceTypeSignature = typeof(IFoo).GetMethod(nameof(IFoo.ReferenceType)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), referenceTypeSignature);
            var arrangment = new ReturnValueSequenceArrangement<int>(valueTypeSignature, new List<int>(new[] { 42 }));

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