using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Contracts
{
    public interface IProjectionSpecification<TEntity,Tkey,TResult>:ISpecification<TEntity,Tkey>   where TEntity :  BaseEntity<Tkey>
    {
        Expression<Func<TEntity, TResult>> Selector { get; }
    }
}
