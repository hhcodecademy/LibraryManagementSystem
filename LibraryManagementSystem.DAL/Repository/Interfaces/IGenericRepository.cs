using LibraryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        void Delete(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
    }
}
