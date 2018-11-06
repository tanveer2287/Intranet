#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain.Interface;
using Core.Domain;

#endregion

namespace Core.Repository
{
    public interface IReader<TEntity> where TEntity : IEntity
    {
        /*
        all implmentation
        Refer: http://www.getcodesamples.com/src/2D6ED0E8/1899F1B1
        */
        IQueryable<TEntity> All(
            Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes
        );

     
        TEntity FindBy(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes);

        
        TEntity FindBy(object id);

        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> ExecuteSqlQuery(string sql);


     

    }
}