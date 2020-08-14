using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class Repository<T, TContext> : IRepository<T, TContext>
        where T : BaseEntity //for using IsDelete and Id methods
        where TContext : DbContext //for using update
    {
        public TContext context { get; }
        public DbSet<T> set { get; }
        public Repository(TContext context)
        {
            this.context = context ?? throw new ArgumentNullException();
            set = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return set.Where(x => x.IsDeleted == false).ToList();
        }
        public T Get(int id)
        {
            return set.Find(id);
        }
        public void Create(T item)
        {
            set.Add(item);
        }
        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var obj = (from i in set
                       where i.Id == id /*&& i.IsDeleted == false*/
                       select i).Single();
            if (obj is null)
                return;
            set.Remove(obj);
        }

        public int GetCount()
        {
            return set.Count();
        }

        public IEnumerable<T> GetRange(int StartPosition, int FinishPosition)
        {
            var list = new List<T>();
            var dbList = GetAll().ToList();
            for (int i = StartPosition;
                i < FinishPosition && i < GetCount();
                i++)
                list.Add(dbList[i]);
            return list;
        }

    }
}
