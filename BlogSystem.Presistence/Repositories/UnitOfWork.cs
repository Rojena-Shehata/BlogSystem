using BlogSystem.Domain.Contrcts;
using BlogSystem.Domain.Entities;
using BlogSystem.Presistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        Dictionary<string,object> _repositories = [];
        private readonly BlogDbContext _dbContext;

        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var entityType=typeof(TEntity).Name;
            if(_repositories.TryGetValue(entityType, out var newRepository))
            {
                return (IGenericRepository<TEntity,Tkey>)newRepository;
            }
            var Repository = new GenericRepository<TEntity, Tkey>(_dbContext);
            _repositories.Add(entityType, Repository);
            return Repository;
        }

        public async Task<int> SaveChangesAsync()
        {
          return await  _dbContext.SaveChangesAsync();
        }
    }
}
