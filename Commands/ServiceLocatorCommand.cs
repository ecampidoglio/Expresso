using System;
using System.Management.Automation;
using Autofac;
using AutofacContrib.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// Represents the base class for cmdlets that actively retrieve their dependencies thorugh an <see cref="IServiceLocator"/>.
    /// </summary>
    public class ServiceLocatorCommand : Cmdlet
    {
        private IServiceLocator serviceLocator;
        private IContainer container;
        private ContainerBuilder builder;

        /// <summary>
        /// Gets the singleton <see cref="IServiceLocator"/>.
        /// </summary>
        public IServiceLocator ServiceLocator
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
        public void SetServiceLocator(IServiceLocator instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            serviceLocator = instance;
        }

        private void InitializeServiceLocator()
        {
            InitializeContainer();
            serviceLocator = new AutofacServiceLocator(container);
        }

        private void InitializeContainer()
        {
            InitializeContainerBuilder();
            container = builder.Build();
        }

        private void InitializeContainerBuilder()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<Data.DataContext>().As<Data.IUnitOfWork>();
            builder.RegisterGeneric(typeof(Data.Repository<>)).As(typeof(Data.IRepository<>));
        }
    }
}