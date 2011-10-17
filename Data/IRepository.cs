using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Defines the interface used to access instances of the specified <typeparamref name="TEntity"/> type from the data store.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity instances to access.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
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
        IEnumerable<TEntity> Find(params string[] includedPropertyPaths);

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
        "CA1006:DoNotNestGenericTypesInMemberSignatures",
        Justification = "This syntax is required by 'Expression<TDelegate>' lambda expressions")]
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> criteria, params string[] includedPropertyPaths);

        /// <summary>
        /// Persists the data contained in the specified <typeparamref name="TEntity"/> instance.
        /// </summary>
        /// <param name="instance">The instance containing the data to persist.</param>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is null.</exception>
        void Save(TEntity instance);
    }

    /// <summary>
    /// Defines the interface used to access loosely typed object instances from the data store.
    /// </summary>
    public interface IRepository : IRepository<object>
    {
    }
}
