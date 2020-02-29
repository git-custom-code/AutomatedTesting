namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="ArrangementCollection"/> type.
    /// </summary>
    public sealed class ArrangementCollectionTests
    {
        [Fact(DisplayName = "Create an empty arrangement collection")]
        public void CreateEmptyArrangementCollection()
        {
            // Given

            // When
            var arrangmentCollection = new ArrangementCollection();

            // Then
            Assert.Empty(arrangmentCollection);
            Assert.Equal(0u, arrangmentCollection.Count);
        }

        [Fact(DisplayName = "Create a new arrangement collection")]
        public void CreateNewArrangementCollection()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<int>(signature, 42);

            // When
            var arrangmentCollection = new ArrangementCollection(arrangment);

            // Then
            Assert.Single(arrangmentCollection);
            Assert.Equal(1u, arrangmentCollection.Count);
        }

        [Fact(DisplayName = "Add an new arrangement to the collection")]
        public void AddNewArrangementToCollection()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var arrangment = new ReturnValueArrangement<int>(signature, 42);
            var arrangmentCollection = new ArrangementCollection();

            // When
            arrangmentCollection.Add(arrangment);

            // Then
            Assert.Single(arrangmentCollection);
            Assert.Equal(1u, arrangmentCollection.Count);
        }

        [Fact(DisplayName = "Applies only the matchings arrangment to the invocation of a method")]
        public void OnlyApplyMatchingArrangement()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var ohterSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithOneParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);
            var otherArrangment = new ReturnValueArrangement<int>(ohterSignature, 13);
            var arrangmentCollection = new ArrangementCollection(arrangment, otherArrangment);

            // When
            arrangmentCollection.ApplyTo(invocation);

            // Then
            Assert.Equal(42, invocation.ReturnValue);
        }

        [Fact(DisplayName = "Check if a matching arrangment exists for a method invocation")]
        public void CheckIfAMatchingArrangmentExists()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var otherSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithOneParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);
            var otherArrangment = new ReturnValueArrangement<int>(otherSignature, 13);
            var arrangmentCollection = new ArrangementCollection(arrangment, otherArrangment);

            // When
            var hasMatchingArrangment = arrangmentCollection.CanApplyAtLeasOneArrangmentTo(invocation);

            // Then
            Assert.True(hasMatchingArrangment);
        }

        [Fact(DisplayName = "Try to apply only the matching arrangments to the invocation of a method")]
        public void TryToApplyOnlyMatchingArrangements()
        {
            // Given
            var signature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithoutParameter)) ?? throw new InvalidOperationException();
            var otherSignature = typeof(IFooWithValueTypeFunc)
                .GetMethod(nameof(IFooWithValueTypeFunc.MethodWithOneParameter)) ?? throw new InvalidOperationException();
            var invocation = new FuncInvocation(new Dictionary<ParameterInfo, object>(), signature);
            var arrangment = new ReturnValueArrangement<int>(signature, 42);
            var otherArrangment = new ReturnValueArrangement<int>(otherSignature, 13);
            var arrangmentCollection = new ArrangementCollection(arrangment, otherArrangment);

            // When
            var hasAppliedArrangment = arrangmentCollection.TryApplyTo(invocation);

            // Then
            Assert.True(hasAppliedArrangment);
            Assert.Equal(42, invocation.ReturnValue);
        }
    }
}