using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Thoughtology.Expresso.Tests.Foundation
{
    public class DbSetStub<TEntity> : IDbSet<TEntity>
        where TEntity : class
    {
        public DbSetStub()
        {
            this.Entities = new List<TEntity>();
        }

        public ObservableCollection<TEntity> Local
        {
            get { return new ObservableCollection<TEntity>(Entities); }
        }

        public Type ElementType
        {
            get { return Entities.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return Entities.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Entities.AsQueryable().Provider; }
        }

        internal ICollection<TEntity> Entities { get; set; }

        public TEntity Add(TEntity entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public TEntity Remove(TEntity entity)
        {
            Entities.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}