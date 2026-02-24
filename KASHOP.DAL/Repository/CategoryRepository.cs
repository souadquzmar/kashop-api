using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        public List<Category> GetAll()
        {
            var categories = _context.Categories.Include(c=>c.Translations).ToList();
            return categories;
        }
    }
}