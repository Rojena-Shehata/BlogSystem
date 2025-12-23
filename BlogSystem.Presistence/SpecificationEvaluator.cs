using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity,Tkey>(this IQueryable<TEntity> inputQueryable, ISpecification<TEntity,Tkey> specification)where TEntity : BaseEntity<Tkey>
        {
            var query = inputQueryable;
            if (specification is not null)
            {
                if(specification.Criteria is not null)
                {
                    query = query.Where(specification.Criteria);
                }
                if (specification.AsNoTracking)
                {
                    query=query.AsNoTracking();
                }
                if(specification.InCludeExpressions is not null)
                {
                    if (specification.InCludeExpressions?.Count > 1)
                    {
                        query = query.AsSplitQuery();
                    }
                    foreach (var inCludeExpression in specification.InCludeExpressions)
                    {
                        query = query.Include(inCludeExpression);

                    }
                }
            }
             return query;
        }
        public static IQueryable<TResult> GetQuery<TEntity,Tkey,TResult>(this IQueryable<TEntity> inputQueryable, IProjectionSpecification<TEntity,Tkey, TResult> specification)where TEntity : BaseEntity<Tkey>
        {
            var query = inputQueryable;
            if (specification is not null)
            {
                //criteria
                if(specification.Criteria is not null)
                {
                    query = query.Where(specification.Criteria);
                }
                //AsNoTracking
                if (specification.AsNoTracking)
                {
                    query = query.AsNoTracking();
                }
                //AsNoTracking
                if (specification.InCludeExpressions is not null&&specification.Selector is null)
                {
                    if (specification.InCludeExpressions?.Count > 1)
                    {
                        query = query.AsSplitQuery();
                    }
                   
                    foreach (var inCludeExpression in specification.InCludeExpressions)
                    {
                        query = query.Include(inCludeExpression);

                    }
                }
            }
            query = query.AsSplitQuery();
                   var newQuery=query.Select(specification.Selector);
                    return newQuery;
                
                  }

    }
}
