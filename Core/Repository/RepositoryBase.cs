using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository
{
    public abstract class RepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly DbContext Context;

        protected RepositoryBase(DbContext dbContext)
        {
            Context = dbContext;
        }

        /// <summary>
        ///     Returns a IQueryable
        ///     <T>
        ///         with includes
        ///         refer http://www.appetere.com/Blogs/SteveM/May-2012/Passing-Include-statements-into-a-Writer
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        protected IQueryable<TEntity> CreateIncludedSet(
            IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            var set = CreateSet();
            return includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (set, (current, expression) => current.Include(expression));
        }

        protected DbSet<TEntity> CreateSet()
        {
            return Context.Set<TEntity>();
        }
    }
}
