using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Update<TEntity>(TEntity user);
        Task<bool> SaveChangesAsync<TEntity>();

        Task<IEnumerable<TEntity>> GetAll<TEntity>();

        Task<TEntity> GetById<TEntity>(int id);

        Task<TEntity> GetByName<TEntity>(string name);
    }
}