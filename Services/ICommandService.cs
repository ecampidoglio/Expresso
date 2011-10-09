namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Defines the command operations that can be performed on entities of the specified <typeparamref name="TEntity"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities involved in the commands.</typeparam>
    public interface ICommandService<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Persists the contents of the specified entity.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        void Save(TEntity entity);
    }
}
