using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Implements the <see cref="IRepository&lt;TEntity&gt;"/> interface using <see cref="IUnitOfWork"/>
    /// objects to create sessions that encapsulate multiple operations against the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity instances to access.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The <see cref="IUnitOfWork"/> used to perform groups of operations against a data store as a single unit.
        /// </param>
        /// <exception cref="ArgumentNullException">unitOfWork is null.</exception>
        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all instances of the specified entity type matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the results.</param>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of the specified entity type
        /// or an empty sequence when none matched the criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">criteria is null.</exception>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> criteria, params string[] includedPropertyPaths)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all instances of the specified entity type.
        /// </summary>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of the specified entity type
        /// or an empty sequence when none were found.
        /// </returns>
        public IEnumerable<TEntity> FindAll(params string[] includedPropertyPaths)
        {
            return unitOfWork.Get<TEntity>();
        }

        /// <summary>
        /// Retrieves an instance of the specified entity type matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the results.</param>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// An instance of the specified entity type
        /// or <strong>Null</strong> when none matched the criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">criteria is null.</exception>
        public TEntity FindOne(Expression<Func<TEntity, bool>> criteria, params string[] includedPropertyPaths)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persists the data contained in the specified entity instance.
        /// </summary>
        /// <param name="instance">The instance containing the data to persist.</param>
        /// <exception cref="ArgumentNullException">instance is null.</exception>
        public void Save(TEntity instance)
        {
            throw new NotImplementedException();
        }
    }
}