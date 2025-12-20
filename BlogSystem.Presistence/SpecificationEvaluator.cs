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

                if(specification.InCludeExpression is not null)
                {
                    query = query.Include(specification.InCludeExpression);
                }
            }
             return query;
        }
    }
}
