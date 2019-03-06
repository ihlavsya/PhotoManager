using PhotoManager.DAL.EF;
using PhotoManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager.DAL.Repositories
{
    public class GenericRepository <TEntity>: IRepository<TEntity> where TEntity : class
    {
        protected readonly PhotoManagerContext _dataContext;
        protected readonly IDbSet<TEntity> _dbset;

        public GenericRepository(PhotoManagerContext context)
        {
            _dataContext = context;
            _dbset = _dataContext.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<TEntity> GetSortedPhotosByUpdateDateTime(Func<TEntity, bool> predicate)
        {
            return _dbset.OrderByDescending(predicate);
        }

        public virtual TEntity Get(Func<TEntity, bool> predicate)
        {
            return _dbset.Where(predicate).FirstOrDefault<TEntity>();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _dbset.Where(predicate);
        }
        public virtual void Create(TEntity entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            TEntity item = _dbset.Find(id);
            if (item != null)
            {
                _dbset.Remove(item);
                _dataContext.SaveChanges();
            }
        }
    }
}
