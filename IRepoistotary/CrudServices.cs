using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ajay.PMS.Data;
using Ajay.PMS.Irepository;
using Ajay.PMS.Models;

using Microsoft.EntityFrameworkCore;

namespace Ajay.PMS.Repository.CRUD
{
    public class CrudService<T> : ICrudServices<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public CrudService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Delete(T entity)
        {
            var result = _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return result.Entity.Id;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id)
;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).SingleOrDefaultAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<int> UpdateAsync(T entity)
        {
            var result = _context.Set<T>().Update(entity).Property(p => p.Id);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

    }
}
