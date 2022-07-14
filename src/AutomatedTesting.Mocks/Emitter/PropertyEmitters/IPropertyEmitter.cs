namespace CustomCode.AutomatedTesting.Mocks.Emitter;

/// <summary>
/// Property emitter implementations can be used to dynamically create properties for a
/// dynamic proxy that forward their calls to an injected <see cref="Interception.IInterceptor"/>
/// instance.
/// </summary>
public interface IPropertyEmitter
{
    /// <summary>
    /// Emits the code for the dynamic property implementation.
    /// </summary>
    void EmitPropertyImplementation();
}
