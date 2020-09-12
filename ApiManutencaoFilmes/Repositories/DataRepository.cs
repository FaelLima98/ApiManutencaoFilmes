using ApiManutencaoFilmes.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiManutencaoFilmes.Repositories {
    public class DataRepository<T> : IDataRepository<T> where T : class {

        private readonly ApiManutencaoFilmesContext _context;

        public DataRepository(ApiManutencaoFilmesContext context) {
            _context = context;
        }

        public IQueryable<T> Get() {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate) {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity) {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity) {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> SaveAsync(T entity) {
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
