using AuthServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Data.Repositories
{
    public class GenericRepository<TEntitiy> : IGenericRepository<TEntitiy> where TEntitiy : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntitiy> _dbset;

        public GenericRepository(AppDbContext context, DbSet<TEntitiy> dbset)
        {
            _context = context;
            _dbset = context.Set<TEntitiy>();
        }

        public async Task AddAsync(TEntitiy entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntitiy>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<TEntitiy> GetByIdAsync(int id)
        {
            var entity = await _dbset.FindAsync(id);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public void Remove(TEntitiy entity)
        {
            _dbset.Remove(entity);
        }

        public TEntitiy Update(TEntitiy entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntitiy> Where(Expression<Func<TEntitiy, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
    }
}
