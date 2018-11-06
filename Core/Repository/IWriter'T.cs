#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Interface;

#endregion

namespace Core.Repository
{
    public interface IWriter<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        bool Add(TAggregateRoot entity);
        Task<bool> AddAsync(TAggregateRoot entity);
        bool Add(IEnumerable<TAggregateRoot> items);
        Task<bool> AddAsync(IEnumerable<TAggregateRoot> items);
        bool Update(TAggregateRoot entity);
        bool Update(IEnumerable<TAggregateRoot> entities);
        Task<bool> UpdateAsync(TAggregateRoot entity);
        bool Delete(TAggregateRoot entity);
        bool Delete(IEnumerable<TAggregateRoot> entities);
    }
}