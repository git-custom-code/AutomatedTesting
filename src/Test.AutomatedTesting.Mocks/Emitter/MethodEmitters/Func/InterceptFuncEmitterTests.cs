namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Context;
    using Core.Extensions;
    using CustomCode.AutomatedTesting.Mocks.Interception.ReturnValue;
    using Interception;
    using Interception.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="InterceptFuncEmitter{T}"/> type.
    /// </summary>
    public sealed partial class InterceptFuncEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public InterceptFuncEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        #region Interceptor

        private sealed class FuncInterceptor<T> : IInterceptor
        {
            public FuncInterceptor([AllowNull] T value)
            {
                Value = value;
            }

            [AllowNull, MaybeNull]
            private T Value { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<T>>(out var feature))
                {
                    feature.ReturnValue = Value;
                    return true;
                }

                return false;
            }
        }

        private sealed class ReplaceRefParameterInterceptor<T> : IInterceptor
        {
            public ReplaceRefParameterInterceptor([AllowNull] T value)
            {
                Value = value;
            }

            [AllowNull, MaybeNull]
            private T Value { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IParameterRef>(out var parameterRef))
                {
                    foreach (var parameter in parameterRef.RefParameterCollection)
                    {
                        if (parameter.Type.IsValueType)
                        {
                            parameter.Value = Activator.CreateInstance(parameter.Type);
                        }
                        else
                        {
                            parameter.Value = null;
                        }
                    }
                }

                if (invocation.TryGetFeature<IReturnValue<T>>(out var feature))
                {
                    feature.ReturnValue = Value;
                }

                return true;
            }
        }

        private sealed class OutParameterInterceptor<T> : IInterceptor
        {
            public OutParameterInterceptor([AllowNull] T result, [AllowNull] T value)
            {
                Result = result;
                Value = value;
            }

            [AllowNull, MaybeNull]
            private T Result { get; }

            [AllowNull, MaybeNull]
            private T Value { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IParameterOut>(out var parameterOut))
                {
                    foreach (var parameter in parameterOut.OutParameterCollection)
                    {
                        parameter.Value = Value;
                    }
                }
                if (invocation.TryGetFeature<IReturnValue<T>>(out var feature))
                {
                    feature.ReturnValue = Result;
                }

                return true;
            }
        }

        #endregion
    }
}