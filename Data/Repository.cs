﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <exception cref="ArgumentNullException"><paramref name="unitOfWork"/> is null.</exception>
        public Repository(IUnitOfWork unitOfWork)
        {
            VerifyUnitOfWorkIsNotNull(unitOfWork);
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all instances of the entity type.
        /// </summary>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified <typeparamref name="TEntity"/> type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of <typeparamref name="TEntity"/>
        /// or an empty sequence when none were found.
        /// </returns>
        public IEnumerable<TEntity> Find(params string[] includedPropertyPaths)
        {
            return unitOfWork.Get<TEntity>(includedPropertyPaths);
        }

        /// <summary>
        /// Retrieves all instances of the specified <typeparamref name="TEntity"/> type matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the results.</param>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of <typeparamref name="TEntity"/>
        /// or an empty sequence when none matched the criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="criteria"/> is null.</exception>
        public IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> criteria,
            params string[] includedPropertyPaths)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persists the data contained in the specified <typeparamref name="TEntity"/> instance.
        /// </summary>
        /// <param name="instance">The instance containing the data to persist.</param>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is null.</exception>
        public void Save(TEntity instance)
        {
            VerifyEntityIsNotNull(instance);
            RegisterEntityAsAddedOrModified(instance);
            unitOfWork.Commit();
        }

        private void VerifyUnitOfWorkIsNotNull(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
        }

        private void VerifyEntityIsNotNull(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
        }

        private void RegisterEntityAsAddedOrModified(TEntity instance)
        {
            if (IsPersistedEntity(instance))
            {
                unitOfWork.RegisterModified(instance);
            }
            else
            {
                unitOfWork.RegisterAdded(instance);
            }
        }

        private bool IsPersistedEntity(TEntity instance)
        {
            return unitOfWork.Get<TEntity>().Contains(instance);
        }
    }
}
