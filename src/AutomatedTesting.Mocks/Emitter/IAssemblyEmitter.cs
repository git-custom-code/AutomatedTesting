namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using System;

    /// <summary>
    /// Assembly emitters can be used to create a dynamic in-memory assembly which in turn can be used
    /// to dynamically create (proxy) types at runtime.
    /// </summary>
    public interface IAssemblyEmitter
    {
        /// <summary>
        /// Create a new dynamic type (inside of a dynamically created in-memory assembly) with the given
        /// name (and namespace).
        /// </summary>
        /// <param name="typeFullName"> The full name (namespace + type name) of the type to be created. </param>
        /// <returns> The newly created dynamic type. </returns>
        Type EmitType(string typeFullName);
    }
}