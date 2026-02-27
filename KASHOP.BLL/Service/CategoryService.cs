using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;

namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryResponses = categories.Adapt<List<CategoryResponse>>();
            return categoryResponses;
        }
    }
}