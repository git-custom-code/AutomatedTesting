namespace CustomCode.AutomatedTesting.Mocks
{
    using Dependencies;
    using LightInject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Static entry point for the fluent api that allows the creation of <see cref="IMocked{T}"/> instances.
    /// </summary>
    public static class Mock
    {
        #region Dependencies

        /// <summary>
        /// Static initialization for dependencies of the <see cref="Mock"/> type.
        /// </summary>
        static Mock()
        {
            using var iocContainer = new ServiceContainer();
            iocContainer.RegisterAssembly(typeof(IMockedDependencyFactory).Assembly);
            DependencyFactory = iocContainer.GetInstance<IMockedDependencyFactory>();
        }

        /// <summary>
        /// Gets a factory to create <see cref="IMockedDependency{T}"/> instances.
        /// </summary>
        private static IMockedDependencyFactory DependencyFactory { get; }

        #endregion

        #region Logic

        /// <summary>
        /// Create a <see cref="IMocked{T}"/> instance for the given type <typeparamref name="T"/> whose
        /// dependencies will be replaced by mocks with the specified <paramref name="behavior"/>.
        /// </summary>
        /// <typeparam name="T"> The signature of the type under test. </typeparam>
        /// <param name="behavior"> The behavior for the mocked dependencies. </param>
        /// <returns> A new mocked instance of the type under test. </returns>
        public static IMocked<T> CreateMocked<T>(MockBehavior behavior = MockBehavior.Loose) where T : class
        {
            var type = typeof(T);
            if (type.IsClass == false)
            {
                throw new ArgumentException($"Type {type.Name} must be a class");
            }

            var constructor = SelectConstructor(type);
            var mockedDependencies = new List<IMockedDependency>();
            foreach (var dependency in constructor.GetParameters())
            {
                var mockedDependency = DependencyFactory.CreateMockedDependency(dependency.ParameterType, behavior);
                mockedDependencies.Add(mockedDependency);
            }

            var instance = (T)constructor.Invoke(mockedDependencies.Select(mock => mock.Instance).ToArray());
            return new Mocked<T>(instance, mockedDependencies);
        }

        /// <summary>
        /// Select the appropriate public constructor for the given <paramref name="type"/> that
        /// will be used to creat the <see cref="IMocked{T}"/> instance.
        /// </summary>
        /// <param name="type"> The type to be mocked. </param>
        /// <returns> The signature of the constructor that will be used to create a new typye instance. </returns>
        private static ConstructorInfo SelectConstructor(Type type)
        {
            // ToDo: Better .ctor selection logic
            var publicConstructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (publicConstructors == null || publicConstructors.Length == 0)
            {
                throw new ArgumentException($"Type {type.Name} does not contain a non-static public ctor");
            }
            else if (publicConstructors.Length > 1)
            {
                throw new ArgumentException($"Type {type.Name} must have exactly one non-static public ctor");
            }

            return publicConstructors[0];
        }

        #endregion
    }
}