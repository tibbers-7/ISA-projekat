using BloodBankAPI.Model;
using System.Linq.Expressions;

namespace BloodBankAPI.Repository
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(string expression);
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T,bool>> expression);
    }
}
