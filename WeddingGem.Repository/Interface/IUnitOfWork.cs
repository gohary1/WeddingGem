using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Repository.Interface
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenaricRepository<TEntity> Repository<TEntity>() where TEntity :class;
        
    }
}
