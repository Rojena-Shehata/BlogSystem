using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
    {
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity,Tkey> specification);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,Tkey> specification);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        void Add(TEntity entity);


    }
}
