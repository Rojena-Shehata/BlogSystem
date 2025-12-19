using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Contrcts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, Tkey> GetRepository<TEntity,Tkey>(TEntity entity) where TEntity:BaseEntity<Tkey>;

        Task<int> SaveChangesAsync();
    }
}
