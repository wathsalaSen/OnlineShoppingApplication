using OnlineShopping.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Common.Interfaces
{
    public interface IAsyncRepository<T> where T : Class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        //Task<T> GetSingleBySpecAsync(ISpecification<T> spec);
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : Class;
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //Task<int> CountAsync(ISpecification<T> spec);
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> expression) where T : class;
    }
}
