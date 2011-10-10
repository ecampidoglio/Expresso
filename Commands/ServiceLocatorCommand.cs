using System;
using System.Management.Automation;
using Autofac;
using AutofacContrib.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Thoughtology.Expresso.Data;

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
        private ErrorRecord errorRecord;

        /// <summary>
        /// Gets or sets the connection string to use to connect to the data store.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string ConnectionString { get; set; }

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

        /// <summary>
        /// Throws the specified <see cref="Exception"/> as a terminating error for the cmdlet.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to throw.</param>
        protected void ThrowError(Exception e)
        {
            ConvertExceptionToErrorRecord(e);
            ThrowTerminatingError(errorRecord);
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
            RegisterComponents();
        }

        private void RegisterComponents()
        {
            RegisterUnitOfWork();
            RegisterRepositories();
        }

        private void RegisterUnitOfWork()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                builder.RegisterType<DataContext>().As<Data.IUnitOfWork>();
            }
            else
            {
                builder.Register(c => new DataContext(ConnectionString)).As<IUnitOfWork>();
            }
        }

        private void RegisterRepositories()
        {
            builder.RegisterGeneric(typeof(Data.Repository<>)).As(typeof(IRepository<>));
        }

        private void ConvertExceptionToErrorRecord(Exception exception)
        {
            if (exception.InnerException != null)
            {
                CreateErrorRecordFromInnerException(exception);
            }
            else
            {
                CreateErrorRecordFromException(exception);
            }
        }

        private void CreateErrorRecordFromException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception);
        }

        private void CreateErrorRecordFromInnerException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception.InnerException, ErrorCategory.InvalidOperation);
        }
    }
}
