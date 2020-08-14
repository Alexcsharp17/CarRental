using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IRepository<T, TContext>
       where T : BaseEntity
       where TContext : DbContext
    {
        /// <summary>
        /// Get a database context
        /// </summary>
        TContext context { get; }
        /// <summary>
        /// Get a set
        /// </summary>
        DbSet<T> set { get; }
        /// <summary>
        /// Get all non-soft-deleted members
        /// </summary>
        /// <returns>Enumerable of Great Entities or its childs</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Entity</returns>
        T Get(int id);
        /// <summary>
        /// Add an item to the database
        /// </summary>
        /// <param name="item">Item to add</param>
        void Create(T item);
        /// <summary>
        /// Update an item in the database
        /// </summary>
        /// <param name="item">Modified item with equal id</param>
        void Update(T item);
        /// <summary>
        /// Delete an item from the database
        /// </summary>
        /// <param name="id">Item id</param>
        void Delete(int id);
        /// <summary>
        /// Get count of non-soft-deleted items
        /// </summary>
        /// <returns>Count of items</returns>
        int GetCount();
        /// <summary>
        /// Get a gap in item collection
        /// </summary>
        /// <param name="StartPosition">Start position</param>
        /// <param name="FinishPosition">Finish position</param>
        /// <returns>Enumerable of items</returns>
        IEnumerable<T> GetRange(int StartPosition, int FinishPosition);
    }
}
