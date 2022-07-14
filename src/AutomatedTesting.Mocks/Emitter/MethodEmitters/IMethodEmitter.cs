namespace CustomCode.AutomatedTesting.Mocks.Emitter;

/// <summary>
/// Method emitter implementations can be used to dynamically create methods for a
/// dynamic proxy that forward their calls to an injected <see cref="Interception.IInterceptor"/>
/// instance.
/// </summary>
public interface IMethodEmitter
{
    /// <summary>
    /// Emits the code for the dynamic method implementation.
    /// </summary>
    void EmitMethodImplementation();
}
