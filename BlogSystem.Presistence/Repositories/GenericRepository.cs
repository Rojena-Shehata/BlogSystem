using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using BlogSystem.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presistence.Repositories
{
    public class GenericRepository <TEntity, TKey>(BlogDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private DbSet<TEntity> _dbSet=_dbContext.Set<TEntity>();




        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }


        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
            return await _dbSet.GetQuery<TEntity, TKey>(specification).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            return await _dbSet.GetQuery(specification).FirstOrDefaultAsync();
        }
    }
}
