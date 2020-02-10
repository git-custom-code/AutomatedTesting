namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using Arrangements;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of the <see cref="IInterceptor"/> interface for mocked dependency instances that will return
    /// default values for each intercepted method and property.
    /// </summary>
    public sealed class LooseMockInterceptor : IInterceptor
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="LooseMockInterceptor"/> type.
        /// </summary>
        /// <param name="arrangements"> A collection of <see cref="IArrangement"/>s for the intercepted calls. </param>
        public LooseMockInterceptor(IArrangementCollection arrangements)
        {
            Arrangements = arrangements;
        }

        /// <summary>
        /// Gets a collection of <see cref="IArrangement"/>s for the intercepted calls.
        /// </summary>
        private IArrangementCollection Arrangements { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void Intercept(IInvocation invocation)
        {
            if (Arrangements.TryApplyTo(invocation) == false)
            {
                if (invocation is FuncInvocation funcInvocation)
                {
                    var @default = GetDefault(funcInvocation.Signature.ReturnType);
                    funcInvocation.ReturnValue = @default;
                }
                else if (invocation is AsyncActionInvocation asyncActionInvocation)
                {
                    asyncActionInvocation.ReturnValue = Task.CompletedTask;
                }
                else if (invocation is AsyncFuncInvocation asyncFuncInvocation)
                {
                    var returnType = asyncFuncInvocation.Signature.ReturnType.GenericTypeArguments[0];
                    var @default = GetDefault(returnType);
                    asyncFuncInvocation.ReturnValue = Task.FromResult(@default);
                }
            }
        }

        /// <summary>
        /// Gets the default value for a given <paramref name="type"/>.
        /// </summary>
        /// <param name="type"> The type whose default value should be returned. </param>
        /// <returns> The default value for the given <paramref name="type"/>. </returns>
        private object? GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        #endregion
    }
}