#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Interface;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace OrgWebAPI.Repository
{
    public class Writer<TEntity> : RepositoryBase<TEntity>, IWriter<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        public Writer(APIContext context) : base(context)
        {
        }

        public bool Add(TEntity entity)
        {
            CreateSet().Add(entity);
            Context.SaveChanges();
            return true;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            CreateSet().Add(entity);
            await Context.SaveChangesAsync();
            return true;
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
                Add(item);
            return true;
        }

        public async Task<bool> AddAsync(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
                await AddAsync(item);
            return true;
        }

        public bool Update(TEntity entity)
        {
            CreateSet().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            /*
             * commented to check audit
            CreateSet().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            */
            Context.SaveChanges();
            return true;
        }

        public bool Update(IEnumerable<TEntity> entities)
        {
            CreateSet().AttachRange(entities);
            //Context.Entry(entities).State = EntityState.Modified;

            Context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            CreateSet().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
            return true;
        }

        public bool Delete(TEntity entity)
        {
            CreateSet().Attach(entity);
            CreateSet().Remove(entity);
            Context.SaveChanges();
            return true;
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
            return true;
        }
    }

    public class Reader<TEntity> : RepositoryBase<TEntity>, IReader<TEntity> where TEntity : Entity
    {
        public Reader(APIContext context) : base(context)
        {
        }

        public IQueryable<TEntity> All(Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var set = CreateIncludedSet(includes);
            return (predicate == null ? set : set.Where(predicate));
        }

       

        public TEntity FindBy(object id)
        {
            return CreateSet().Find(id);
        }

        public IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return All(expression, includes).AsQueryable();
        }

        public TEntity FindBy(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return FilterBy(expression, includes).SingleOrDefault();
        }

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await FilterBy(expression, includes).SingleOrDefaultAsync();
        }

        public IQueryable<TEntity> ExecuteSqlQuery(string sql)
        {
            var readerSet =  Context.Set<TEntity>();
            var result = readerSet.FromSql(sql);
            return result;
        }
    }
}