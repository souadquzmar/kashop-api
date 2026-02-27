using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.BLL.Service;
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
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResources> localizer)
        {
            _categoryService=categoryService;
            _localizer=localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var response = await _categoryService.CreateCategory(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                response
            });
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetAllCategories();
            return Ok(new
            {
                data = response,
                message = _localizer["Success"].Value
            });
        }
    }
}