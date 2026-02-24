using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CategoriesController(ICategoryRepository categoryRepository, IStringLocalizer<SharedResources> localizer)
        {
            _categoryRepository=categoryRepository;
            _localizer=localizer;
        }
        [HttpPost("")]
        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _categoryRepository.Create(category);
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAll();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(new
            {
                data = response,
                message = _localizer["Success"].Value
            });
        }
    }
}