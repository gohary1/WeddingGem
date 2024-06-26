using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Interface
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, T>> Selector { get; set; }
        public Expression<Func<T, object>> orderBy { get; set; }
        public Expression<Func<T, object>> orderByDesc { get; set; }
    }
}
