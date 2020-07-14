using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rapport.Domaine.Interfaces;
using Rapport.Domaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> _dbSet;
        protected string _tableName;
        protected string _primaryKey;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

            var entityType = _context.Model.FindEntityType(typeof(T));
            _tableName = entityType.GetTableName();
            _primaryKey = entityType.FindPrimaryKey().GetName();

        }
        public async Task<T> Authentification(string userLogin, string password)
        {
            var UserLogin = new SqlParameter("UserLogin", userLogin);
            var Password = new SqlParameter("Password", password);

            var query = await _dbSet.FromSqlRaw<T>($"SELECT * FROM dbo.[{_tableName}] WHERE UserLogin = @UserLogin AND Password = @Password", UserLogin, Password).ToListAsync();

            return query.SingleOrDefault();
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = await _dbSet.FromSqlRaw<T>($"SELECT * FROM dbo.[{_tableName}]").ToListAsync();

            return query;
        }

        public async Task<T> GetAsync(Guid id)
        {
            var Id = new SqlParameter("id", id);
            var query = await _dbSet.FromSqlRaw<T>($"SELECT * FROM {_tableName} WHERE Id = @id ", Id).ToListAsync();
            return query.SingleOrDefault();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
