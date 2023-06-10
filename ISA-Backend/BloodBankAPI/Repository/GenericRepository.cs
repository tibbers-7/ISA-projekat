using BloodBankAPI.Model;
using BloodBankAPI.Settings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodBankAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly BloodBankDbContext _context;
        public GenericRepository(BloodBankDbContext context) {
            _context= context;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(string expression)
        {
            return await _context.Set<T>().Include(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression){
           
           return await _context.Set<T>().Where(expression).ToListAsync();
        
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }


        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
