namespace CustomCode.AutomatedTesting.Mocks.Emitter;

using Interception;
using System;
using System.Reflection.Emit;

/// <summary>
/// Dependency emitters can be used to create the dependencies of a dynamic type, i.e. a backing
/// field for each dependcy like e.g. an <see cref="IInterceptor"/> and a public constructor that
/// can be used for injecting the created dependencies.
/// </summary>
public interface IDependencyEmitter
{
    /// <summary>
    /// Creates a readonly field for the <see cref="IInterceptor"/> dependency.
    /// </summary>
    /// <param name="type"> The dynamic type that should be extended. </param>
    /// <returns> A <see cref="FieldBuilder"/> that represents the created field. </returns>
    /// <remarks>
    /// Emits the following source code:
    /// 
    /// <![CDATA[
    /// private readonly IInterceptor _interceptor;
    /// ]]>
    /// </remarks>
    FieldBuilder CreateInterceptorDependency(TypeBuilder type);

    /// <summary>
    /// Creates a readonly field for the decoratee of type <paramref name="decorateeType"/>.
    /// </summary>
    /// <param name="type"> The dynamic type that should be extended. </param>
    /// <param name="decorateeType"> The type of the decoratee. </param>
    /// <returns> A <see cref="FieldBuilder"/> that represents the created field. </returns>
    /// <remarks>
    /// Emits the following source code:
    /// 
    /// <![CDATA[
    /// private readonly DecorateeType _decoratee;
    /// ]]>
    /// </remarks>
    FieldBuilder CreateDecorateeDependency(TypeBuilder type, Type decorateeType);

    /// <summary>
    /// Creates a constructor that contains a parameter for each requested dependency and
    /// assigns the dependecy to the previously created backing field.
    /// </summary>
    /// <param name="type"> The dynamic type that should be extended. </param>
    /// <param name="dependencies"> The dependencies that should be injected and stored in backing fields. </param>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    /// public .ctor(Type1 dependency1, Type2 dependency2, ...)
    /// {
    ///     _dependency1 = dependency1;
    ///     _dependency2 = dependency2;
    ///     ...
    /// }
    /// ]]>
    /// </remarks>
    void CreateConstructor(TypeBuilder type, params FieldBuilder[] dependencies);
}
