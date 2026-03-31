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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
            _productRepository=productRepository;
            _fileService=fileService;
        }
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if(request.MainImage != null)
            {
                var imageName = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imageName;
            }
            await _productRepository.CreateAsync(product);
            return product.Adapt<ProductResponse>();
        }
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync(
                new string[] {nameof(Product.Translations),
                nameof(Product.CreatedBy)}
            );
            var productResponses = products.Adapt<List<ProductResponse>>();
            return productResponses;
        }
    }
}