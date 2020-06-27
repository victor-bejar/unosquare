using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using App.Persistence.Interface;


namespace App.Persistence.Class
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly DbContext _context = null;

        public Repository(DbContext context)
        {
            this._context = context;
        }

        public TEntity Get(int id)
        {
            return this._context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().ToList();
        }


        public void Add(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            this._context.Set<TEntity>().Remove(entity);
        }

    }

}