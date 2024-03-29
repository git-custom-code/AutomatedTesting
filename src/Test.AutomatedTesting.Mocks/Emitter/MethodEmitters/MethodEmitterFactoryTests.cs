namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

using Interception;
using Interception.Async;
using Mocks.ExceptionHandling;
using System.Reflection;
using System.Reflection.Emit;
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
        var (signature, type, interceptor) = CreateParameter<IFooActionValueTypeParameterIn<int>>(
            methodName: nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter));
        var factory = new MethodEmitterFactory();
    
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptActionEmitter>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptFuncEmitter")]
    public void CreateInterceptFuncEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooFuncValueTypeParameterIn<int>>(
            methodName: nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter));
        var factory = new MethodEmitterFactory();

        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptFuncEmitter<int>>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncTaskInvocation>")]
    public void CreateInterceptAsyncTaskEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooTaskValueTypeParameter>(
            methodName: nameof(IFooTaskValueTypeParameter.MethodWithOneParameterAsync));
        var factory = new MethodEmitterFactory();
        
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptAsyncMethodEmitter<AsyncTaskInvocation>>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncValueTaskInvocation>")]
    public void CreateInterceptAsyncValueTaskEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooValueTaskValueTypeParameter>(
            methodName: nameof(IFooValueTaskValueTypeParameter.MethodWithOneParameterAsync));
        var factory = new MethodEmitterFactory();
        
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptAsyncMethodEmitter<AsyncValueTaskInvocation>>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncGenericTaskInvocation>")]
    public void CreateInterceptAsyncGenericTaskEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooGenericTaskValueTypeParameter>(
            methodName: nameof(IFooGenericTaskValueTypeParameter.MethodWithOneParameterAsync));
        var factory = new MethodEmitterFactory();
        
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptAsyncMethodEmitter<AsyncGenericTaskInvocation<int>>>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncGenericValueTaskInvocation>")]
    public void CreateInterceptAsyncGenericValueTaskEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooGenericValueTaskValueTypeParameter>(
            methodName: nameof(IFooGenericValueTaskValueTypeParameter.MethodWithOneParameterAsync));
        var factory = new MethodEmitterFactory();
        
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptAsyncMethodEmitter<AsyncGenericValueTaskInvocation<int>>>(emitter);
    }

    [Fact(DisplayName = "MethodEmitterFactory: InterceptAsyncMethodEmitter<AsyncIEnumerableInvocation>")]
    public void CreateInterceptAsynEnumerableEmitter()
    {
        // Given
        var (signature, type, interceptor) = CreateParameter<IFooAsyncEnumerableValueTypeParameter>(
            methodName: nameof(IFooAsyncEnumerableValueTypeParameter.MethodWithOneParameterAsync));
        var factory = new MethodEmitterFactory();
        
        // When
        var emitter = factory.CreateMethodEmitterFor(signature, type, interceptor);

        // Then
        Assert.NotNull(emitter);
        Assert.IsType<InterceptAsyncMethodEmitter<AsyncIEnumerableInvocation<int>>>(emitter);
    }

    #region Mocks

    private (MethodInfo signature, TypeBuilder type, FieldBuilder interceptor) CreateParameter<T>(string methodName)
    {
        var name = new AssemblyName("DynamicMockAssembly");
        var assembly = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect);
        var module = assembly.DefineDynamicModule("DynamicMockModule");

        var typeBuilder = module.DefineType("DynamicType");
        var interceptor = typeBuilder.DefineField("_interceptor", typeof(IInterceptor), FieldAttributes.Private);

        var type = typeof(T);
        var methodInfo = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        return (methodInfo, typeBuilder, interceptor);
    }

    #endregion
}
