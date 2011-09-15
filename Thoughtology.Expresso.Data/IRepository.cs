using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Defines the interface used to access instances of the specified entity type from the data store.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity instances to access.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Retrieves all instances of the specified entity type matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the results.</param>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of the entity type
        /// or an empty sequence when none matched the criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">criteria is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This syntax is required by 'Expression<TDelegate>' lambda expressions")]
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> criteria, params string[] includedPropertyPaths);

        /// <summary>
        /// Retrieves all instances of the entity type.
        /// </summary>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// A sequence of instances of the entity type
        /// or an empty sequence when none were found.
        /// </returns>
        IEnumerable<TEntity> FindAll(params string[] includedPropertyPaths);

        /// <summary>
        /// Retrieves an instance of the entity type matching the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria used to filter the results.</param>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified entity type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>
        /// An instance of the entity type
        /// or <strong>Null</strong> when none matched the criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">criteria is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This syntax is required by the 'Expression<TDelegate>' lambda expressions")]
        TEntity FindOne(Expression<Func<TEntity, bool>> criteria, params string[] includedPropertyPaths);

        /// <summary>
        /// Persists the data contained in the specified entity instance.
        /// </summary>
        /// <param name="instance">The instance containing the data to persist.</param>
        /// <exception cref="ArgumentNullException">instance is null.</exception>
        void Save(TEntity instance);
    }
}