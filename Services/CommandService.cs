using System;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the command operations that can be performed on entities of the specified <typeparamref name="TEntity"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities involved in the commands.</typeparam>
    public class CommandService<TEntity> : Thoughtology.Expresso.Services.ICommandService<TEntity>
        where TEntity: class
    {
        private readonly IRepository<TEntity> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="repository">The repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="unitOfWork"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is null.</exception>
        public CommandService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Persists the contents of the specified entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Save(entity);
        }
    }
}
