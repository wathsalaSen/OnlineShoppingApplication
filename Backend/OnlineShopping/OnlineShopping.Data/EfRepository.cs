using Microsoft.EntityFrameworkCore;
using OnlineShopping.Common.Entities;
using OnlineShopping.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly OnlineShoppingContext _dbContext;

        public EfRepository(OnlineShoppingContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        //public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        //{
        //    return await ApplySpecification(spec).ToListAsync().ConfigureAwait(false);
        //}
        //public async Task<T> GetSingleBySpecAsync(ISpecification<T> spec)
        //{
        //    return (await ListAsync(spec).ConfigureAwait(false)).FirstOrDefault();
        //}

        //public async Task<int> CountAsync(ISpecification<T> spec)
        //{
        //    return await ApplySpecification(spec).CountAsync();
        //}

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        //private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        //{
        //    SpecificationEvaluator<T> evaluator = new SpecificationEvaluator<T>();
        //    return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        //}
    }
}
