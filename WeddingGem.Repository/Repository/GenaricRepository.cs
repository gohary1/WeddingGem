using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Context;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Repository.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T :class
    {
        private readonly AppDbContext _context;

        public GenaricRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
            var result=await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(T model)
        => _context.Set<T>().Remove(model);

        public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsyncWithSpec(ISpecification<T> spec)
         => await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec) .ToListAsync();
        


        public async Task<T> GetAsync(int id)
         => await _context.Set<T>().FindAsync(id);
        
        public async Task<T> GetAsync(string id)
         => await _context.Set<T>().FindAsync(id);


        public async Task<T> GetAsyncWithSpec(ISpecification<T> spec)
        => await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();
        

        public async Task UpdateAsync(T model)
        => _context.Set<T>().Update(model);
         
    }
}
