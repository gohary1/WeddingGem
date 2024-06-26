using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Repository.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();  
        public Expression<Func<T, object>> orderBy { get; set; } = null;
        public Expression<Func<T, object>> orderByDesc { get; set; } = null;
        public Expression<Func<T, T>> Selector { get; set; }

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void addOrderBy(Expression<Func<T, object>> orderby)
        {
            orderBy=orderby;
        }
        public void addOrderByDesc(Expression<Func<T, object>> orderbydesc)
        {
            orderByDesc=orderbydesc;
        }
        public void ApplySelect(Expression<Func<T, T>> select)
        {
            Selector = select;
        }
    }
}
