using DAL;
using DLA.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLA
{
    public class EFRepository : IRepository
    {
        // Llama a la clase de contexto
        ApplicationDbContext _context;

        // Constructor
        public EFRepository(ApplicationDbContext Context)
        {
            this._context = Context;
        }

        //Dispose
        private bool disposedValue;


        public async Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity result = default(TEntity);
            try
            {
                await _context.Set<TEntity>().AddAsync(toCreate);
                await _context.SaveChangesAsync();
                result = toCreate;
            }

            catch (DbException)
            {
                throw;
            }
            return result;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class
        {
            bool result = false;
            try
            {
                _context.Entry<TEntity>(toDelete).State =EntityState.Deleted;
                 result = await _context.SaveChangesAsync() >0;
            }

            catch (DbException)
            {
                throw;
            }
            return result;
        }

        public void Dispose()
        {
           if (_context != null)
            {
                _context.Dispose();
            }
        }

        public Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> RetreiveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool Result = false;
            try
            {
                _context.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                Result = await _context.SaveChangesAsync() > 0;
            }
            catch (DbException)
            {
                throw;
            }
            return Result;
        }
    }
}
