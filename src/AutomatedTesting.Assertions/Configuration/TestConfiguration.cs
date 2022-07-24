namespace CustomCode.AutomatedTesting.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// Global test configuration that can be overriden via <see cref="TestConfigurationAttribute"/>
    /// or <see cref="GlobalTestConfigurationAttribute"/> to use a custom <see cref="ICallerContext"/>
    /// and/or <see cref="IMessageFormatter"/>.
    /// </summary>
    internal static class TestConfiguration
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="TestConfiguration"/> type.
        /// </summary>
        static TestConfiguration()
        {
            Initialize();
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets a dictionary with custom <see cref="ICallerContext"/>s for specific test methods.
        /// </summary>
        private static Dictionary<string, ICallerContext> CustomCallerContexts { get; } = new Dictionary<string, ICallerContext>();

        /// <summary>
        /// Gets a dictionary with custom <see cref="IMessageFormatter"/>s for specific test methods.
        /// </summary>
        private static Dictionary<string, IMessageFormatter> CustomMessageFormatters { get; } = new Dictionary<string, IMessageFormatter>();

        /// <summary>
        /// Gets or sets the default <see cref="ICallerContext"/> implementation that is used by all tests.
        /// </summary>
        private static ICallerContext DefaultCallerContext { get; set; } = default!;

        /// <summary>
        /// Gets or sets the default <see cref="IMessageFormatter"/> implementation that is used by all tests.
        /// </summary>
        private static IMessageFormatter DefaultMessageFormatter { get; set; } = default!;

        #endregion

        #region Logic

        /// <summary>
        /// Initializes the <see cref="DefaultCallerContext"/> and <see cref="DefaultMessageFormatter"/>.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ICallerContext"/> instance that should be used as default or null to use the
        /// <see cref="RoslynCallerContext"/> type as default.
        /// </param>
        /// <param name="formatter">
        /// A <see cref="IMessageFormatter"/> instance that should be used as default or null to use
        /// the <see cref="MessageFormatter"/> type as default.
        /// </param>
        /// <remarks>
        /// This method is called when you use the <see cref="GlobalTestConfigurationAttribute"/> to
        /// configure an assembly wide <see cref="ICallerContext"/> and/or <see cref="IMessageFormatter"/>.
        /// </remarks>
        internal static void Initialize(
            ICallerContext? context = null,
            IMessageFormatter? formatter = null)
        {
            DefaultCallerContext = context ?? new RoslynCallerContext();
            DefaultMessageFormatter = formatter ?? new MessageFormatter();
        }

        /// <summary>
        /// Gets a custom <see cref="ICallerContext"/> for a test method (specified by <paramref name="testMethodName"/>)
        /// or the <see cref="DefaultCallerContext"/> if no custom context was defined.
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        /// <returns> The specified method's <see cref="ICallerContext"/>. </returns>
        internal static ICallerContext GetCallerContextFor(string testMethodName)
        {
            if (CustomCallerContexts.TryGetValue(testMethodName, out var context))
            {
                return context;
            }

            return DefaultCallerContext;
        }

        /// <summary>
        /// Set a custom <see cref="ICallerContext"/> for a test method (specified by <paramref name="testMethodName"/>).
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        /// <param name="context"> The method's custom <see cref="ICallerContext"/>. </param>
        internal static void SetCallerContextFor(string testMethodName, ICallerContext context)
        {
            if (CustomCallerContexts.ContainsKey(testMethodName))
            {
                CustomCallerContexts[testMethodName] = context;
            }
            else
            {
                CustomCallerContexts.Add(testMethodName, context);
            }
        }

        /// <summary>
        /// Removes a custom <see cref="ICallerContext"/> for a test method (specified by <paramref name="testMethodName"/>)
        /// and use the <see cref="DefaultCallerContext"/> instead.
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        internal static void ResetCallerContextFor(string testMethodName)
        {
            CustomCallerContexts.Remove(testMethodName);
        }

        /// <summary>
        /// Gets a custom <see cref="IMessageFormatter"/> for a test method (specified by <paramref name="testMethodName"/>)
        /// or the <see cref="DefaultMessageFormatter"/> if no custom formatter was defined.
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        /// <returns> The specified method's <see cref="IMessageFormatter"/>. </returns>
        internal static IMessageFormatter GetMessageFormatterFor(string testMethodName)
        {
            if (CustomMessageFormatters.TryGetValue(testMethodName, out var formatter))
            {
                return formatter;
            }

            return DefaultMessageFormatter;
        }

        /// <summary>
        /// Set a custom <see cref="IMessageFormatter"/> for a test method (specified by <paramref name="testMethodName"/>).
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        /// <param name="formatter"> The method's custom <see cref="IMessageFormatter"/>. </param>
        internal static void SetMessageFormatterFor(string testMethodName, IMessageFormatter formatter)
        {
            if (CustomMessageFormatters.ContainsKey(testMethodName))
            {
                CustomMessageFormatters[testMethodName] = formatter;
            }
            else
            {
                CustomMessageFormatters.Add(testMethodName, formatter);
            }
        }

        /// <summary>
        /// Removes a custom <see cref="IMessageFormatter"/> for a test method (specified by <paramref name="testMethodName"/>)
        /// and use the <see cref="DefaultMessageFormatter"/> instead.
        /// </summary>
        /// <param name="testMethodName"> The name of the method under test. </param>
        internal static void ResetMessageFormatterFor(string testMethodName)
        {
            CustomMessageFormatters.Remove(testMethodName);
        }

        #endregion
    }
}
