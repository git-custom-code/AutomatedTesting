namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Context;
    using Interception.ReturnValue;
    using Interception;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="DecorateFuncEmitter{T}"/> type.
    /// </summary>
    public sealed partial class DecorateFuncEmitterTests : IClassFixture<ProxyFactoryContext>
    {
        #region Dependencies

        public DecorateFuncEmitterTests(ProxyFactoryContext context)
        {
            Context = context;
        }

        private ProxyFactoryContext Context { get; }

        #endregion

        #region Interceptor

        private sealed class FuncInterceptor<T> : IInterceptor
        {
            public FuncInterceptor([AllowNull] T value, bool wasIntercepted)
            {
                Value = value;
                WasIntercepted = wasIntercepted;
            }

            [AllowNull, MaybeNull]
            private T Value { get; }

            private bool WasIntercepted { get; }

            public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

            public bool Intercept(IInvocation invocation)
            {
                ForwardedInvocations.Add(invocation);
                if (invocation.TryGetFeature<IReturnValue<T>>(out var feature))
                {
                    feature.ReturnValue = Value;
                }

                return WasIntercepted;
            }
        }

        #endregion
    }
}