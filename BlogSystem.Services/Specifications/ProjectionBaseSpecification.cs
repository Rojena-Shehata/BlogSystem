using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Services.Specifications
{
    internal class ProjectionBaseSpecification<TEntity, Tkey, TResult> : BaseSpecification<TEntity, Tkey>, IProjectionSpecification<TEntity, Tkey, TResult> where TEntity : BaseEntity<Tkey>
    {
        protected ProjectionBaseSpecification(Expression<Func<TEntity, bool>> criteria) : base(criteria)
        {
            AsNoTracking = true;
        }

        public Expression<Func<TEntity, TResult>> Selector { get; private set; }
        public void AddSelector(Expression<Func<TEntity, TResult>> selector)
        {
            Selector = selector;
        }
    }
}
