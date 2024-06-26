using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Repository.Repository
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.orderBy != null)
            {
                query = query.OrderBy(spec.orderBy);
            }
            if (spec.orderByDesc != null)
            {
                query = query.OrderByDescending(spec.orderByDesc);
            }
            query = spec.Includes.Aggregate(query, (current, Include) => current.Include(Include));
            if (spec.Selector != null)
            {
                query = query.Select(spec.Selector);
            }
            return query;
        }

    }
}
