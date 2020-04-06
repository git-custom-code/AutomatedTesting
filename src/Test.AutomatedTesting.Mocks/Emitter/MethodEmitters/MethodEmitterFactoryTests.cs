namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    using Interception.Async;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="MethodEmitterFactory"/> type.
    /// </summary>
    public sealed partial class MethodEmitterFactoryTests
    {
        [Fact(DisplayName = "MethodEmitterFactory: InterceptActionEmitter")]
        public void CreateInterceptActionEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooActionValueTypeParameterIn<int>)
                .GetMethod(nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptActionEmitter>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptFuncEmitter")]
        public void CreateInterceptFuncEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooFuncValueTypeParameterIn<int>)
                .GetMethod(nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptFuncEmitter<int>>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncTaskInvocation>")]
        public void CreateInterceptAsyncTaskEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooTaskValueTypeParameter)
                .GetMethod(nameof(IFooTaskValueTypeParameter.MethodWithOneParameterAsync));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptAsyncMethodEmitter<AsyncTaskInvocation>>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncValueTaskInvocation>")]
        public void CreateInterceptAsyncValueTaskEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooValueTaskValueTypeParameter)
                .GetMethod(nameof(IFooValueTaskValueTypeParameter.MethodWithOneParameterAsync));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptAsyncMethodEmitter<AsyncValueTaskInvocation>>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncGenericTaskInvocation>")]
        public void CreateInterceptAsyncGenericTaskEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooGenericTaskValueTypeParameter)
                .GetMethod(nameof(IFooGenericTaskValueTypeParameter.MethodWithOneParameterAsync));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptAsyncMethodEmitter<AsyncGenericTaskInvocation<int>>>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncGenericValueTaskInvocation>")]
        public void CreateInterceptAsyncGenericValueTaskEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooGenericValueTaskValueTypeParameter)
                .GetMethod(nameof(IFooGenericValueTaskValueTypeParameter.MethodWithOneParameterAsync));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptAsyncMethodEmitter<AsyncGenericValueTaskInvocation<int>>>(emitter);
        }

        [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncIEnumerableInvocation>")]
        public void CreateInterceptAsynEnumerableEmitter()
        {
            // Given
            var factory = new MethodEmitterFactory();
            var methodInfo = typeof(IFooAsyncEnumerableValueTypeParameter)
                .GetMethod(nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));

            // When
#nullable disable
            var emitter = factory.CreateMethodEmitterFor(methodInfo, null, null);
#nullable restore

            // Then
            Assert.NotNull(emitter);
            Assert.IsType<InterceptAsyncMethodEmitter<AsyncIEnumerableInvocation<int>>>(emitter);
        }
    }
}