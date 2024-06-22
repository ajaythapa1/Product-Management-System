using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ajay.PMS.Models;


namespace Ajay.PMS.Irepository
{
    public interface ICrudServices<TEntity> where TEntity : BaseEntity
    {
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(int? id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
        int Delete(TEntity entity);

    }
}

