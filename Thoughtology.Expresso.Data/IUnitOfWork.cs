using System;
using System.Data.Entity;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Defines the role interface used to perform groups of operations against a data store as a single unit.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets or sets a collection of <see cref="Posts"/> entities.
        /// </summary>
        DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        void Commit();
    }
}