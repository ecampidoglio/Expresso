using System;
using System.Configuration;
using System.Reflection;
using Autofac;
using AutofacContrib.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Commands.Runtime
{
    /// <summary>
    /// Represents a central location to obtain instances of components that satisfy the required service interfaces.
    /// </summary>
    public static class ServiceLocator
    {
        private static IServiceLocator serviceLocator;
        private static IContainer container;
        private static ContainerBuilder builder;
        private static Configuration configuration;

        /// <summary>
        /// Gets the singleton <see cref="IServiceLocator"/>.
        /// </summary>
        public static IServiceLocator Current
        {
            get
            {
                if (serviceLocator == null)
                {
                    InitializeServiceLocator();
                }

                return serviceLocator;
            }
        }

        /// <summary>
        /// Sets the specified <see cref="IServiceLocator"/> instance to be the singleton
        /// returned by the <see cref="P:ServiceLocator"/> property.
        /// </summary>
        /// <param name="instance">The <see cref="IServiceLocator"/> instance to assign.</param>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is null.</exception>
        public static void SetCurrentInstance(IServiceLocator instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            serviceLocator = instance;
        }

        private static void InitializeServiceLocator()
        {
            InitializeContainer();
            serviceLocator = new AutofacServiceLocator(container);
        }

        private static void InitializeContainer()
        {
            InitializeContainerBuilder();
            container = builder.Build();
        }

        private static void InitializeContainerBuilder()
        {
            builder = new ContainerBuilder();
            RegisterComponents();
        }

        private static void RegisterComponents()
        {
            RegisterUnitOfWork();
            RegisterRepositories();
        }

        private static void RegisterUnitOfWork()
        {
            string connectionString = GetConnectionStringFromConfigurationFile();

            if (String.IsNullOrEmpty(connectionString))
            {
                builder.RegisterType<DataContext>().As<IUnitOfWork>();
            }
            else
            {
                builder.Register(c => new DataContext(connectionString)).As<IUnitOfWork>();
            }
        }

        private static string GetConnectionStringFromConfigurationFile()
        {
            configuration = ReadConfigurationFromFile();
            return GetConnectionStringFromConfiguration();
        }

        private static Configuration ReadConfigurationFromFile()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return ConfigurationManager.OpenExeConfiguration(assemblyLocation);
        }

        private static string GetConnectionStringFromConfiguration()
        {
            var setting = configuration.ConnectionStrings.ConnectionStrings["Expresso"];
            return (setting != null) ? setting.ConnectionString : null;
        }

        private static void RegisterRepositories()
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}
