using System;
using System.Collections.Generic;
using System.Text;

using AvilokTaskAssignment.Data.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace AvilokTaskAssignment.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public void Remove(T entity)
            => _dbSet.Remove(entity);

        public void Update(T entity)
            => _dbSet.Update(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

    }
}
