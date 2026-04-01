using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using KASHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public BrandsController(IBrandService brandService, IStringLocalizer<SharedResources> localizer)
        {
            _brandService=brandService;
            _localizer=localizer;
        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BrandRequest request)
        {
            var response = await _brandService.CreateBrandAsync(request);
            return Ok(
                new {
                    message=_localizer["Success"].Value,
                    data=response
                    });
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(
                new
                {
                    message=_localizer["Success"].Value,
                    data=brands
                });
        }
    }
}