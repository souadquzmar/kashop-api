using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T category);
    }
}