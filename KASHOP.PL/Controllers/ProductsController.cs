using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ProductsController(IProductService productService, IStringLocalizer<SharedResources> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(new
            {
                message = _localizer["Success"].Value,
                response
            });
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = products
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProduct(p => p.Id == id);
            if (product == null)
                return NotFound(new
                {
                    message = _localizer["NotFound"].Value,
                });
            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = product
            });
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            if (!deleted)
                return NotFound(new
                {
                    message = _localizer["NotFound"].Value
                });
            return Ok(new
            {
                message = _localizer["Success"].Value
            });
        }
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateRequest request)
        {
            var updated = await _productService.UpdateProduct(id, request);
            if (!updated)
                return BadRequest();
            return Ok(new { message = _localizer["Success"].Value });
        }
        [HttpPatch("{id}/status")]
        [Authorize]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var updated = await _productService.ToggleStatus(id);
            if (!updated)
                return BadRequest();
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}