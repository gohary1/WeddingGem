using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Context;
using WeddingGem.Data.Entites;
using WeddingGem.Repository.Interface;

namespace WeddingGem.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repo;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repo = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        =>await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _context.DisposeAsync();

        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type=typeof(TEntity).Name;
            if (!_repo.ContainsKey(type))
            {
                var repos = new GenaricRepository<TEntity>(_context);
                _repo.Add(type, repos);
            }
            return (GenaricRepository<TEntity>) _repo[type];
        }
    }
}
