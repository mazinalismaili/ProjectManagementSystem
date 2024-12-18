using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public MainRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;

            if (typeof(T) == typeof(Project))
            {
                query = (IQueryable<T>)query.Include(p => ((Project)(object)p).Employee);
                query = query.Include(p => ((Project)(object)(object)p).Priority);
                query = query.Include(p => ((Project)(object)(object)p).Todos);
            }
            if (typeof(T) == typeof(Comment))
            {
                query = (IQueryable<T>)query.Include(p => ((Comment)(object)p).Employee);
                query = query.Include(p => ((Comment)(object)(object)p).Todo);
            }
            if (typeof(T) == typeof(Todo))
            {
                query = (IQueryable<T>)query.Include(p => ((Todo)(object)p).Employee);
                query = query.Include(p => ((Todo)(object)(object)p).Status);
                query = query.Include(p => ((Todo)(object)(object)p).Project);
                query = query.Include(p => ((Todo)(object)(object)p).Comments).ThenInclude(e => e.Employee);
            }

            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;

            if (typeof(T) == typeof(Project))
            {
                query = (IQueryable<T>)query.Include(p => ((Project)(object)p).Employee);
                query = query.Include(p => ((Project)(object)(object)p).Priority);
            }
            if (typeof(T) == typeof(Comment))
            {
                query = (IQueryable<T>)query.Include(p => ((Comment)(object)p).Employee);
                query = query.Include(p => ((Comment)(object)(object)p).Todo);
            }
            if (typeof(T) == typeof(Todo))
            {
                query = (IQueryable<T>)query.Include(p => ((Todo)(object)p).Employee);
                query = query.Include(p => ((Todo)(object)(object)p).Status);
                query = query.Include(p => ((Todo)(object)(object)p).Project);
                query = query.Include(p => ((Todo)(object)(object)p).Comments);
            }


            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
