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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        public BrandService(IBrandRepository brandRepository, IFileService fileService)
        {
            _brandRepository=brandRepository;
            _fileService=fileService;
        }
        public async Task<BrandResponse> CreateBrandAsync (BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
            if(request.Logo != null)
            {
                var logo = await _fileService.UploadAsync(request.Logo);
                brand.Logo=logo;
            }
            await _brandRepository.CreateAsync(brand);
            return brand.Adapt<BrandResponse>();
        }
        public async Task<List<BrandResponse>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync(
                new string []
                {
                    nameof(Brand.Translations),
                    nameof(Brand.CreatedBy)
                });
            return brands.Adapt<List<BrandResponse>>();
        }
    }
}