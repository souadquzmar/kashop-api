using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            //Include(c=>c.Translations)
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }
    }
}