using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Interface
{
    public interface IGenaricRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> spec);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsyncWithSpec(ISpecification<T> spec);
        public Task<T> GetAsync(int id);
        public Task<T> GetAsync(string id);
        public Task<int> AddAsync(T model);
        public Task UpdateAsync(T model);
        public Task DeleteAsync(T model);
    }
}
