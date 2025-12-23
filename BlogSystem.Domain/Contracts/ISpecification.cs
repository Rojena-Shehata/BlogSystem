using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity,bool>> Criteria {  get; }
        ICollection<Expression<Func<TEntity, object>>> InCludeExpressions { get;  }
        bool AsNoTracking { get; set; }


    }
}
