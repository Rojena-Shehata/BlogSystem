using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Services.Specifications
{
    internal class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>> Criteria {  get;private set; }

        public Expression<Func<TEntity, object>> InCludeExpression {  get;private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> inCludeExpression)
        {
            InCludeExpression = inCludeExpression;
        }
    }
}
