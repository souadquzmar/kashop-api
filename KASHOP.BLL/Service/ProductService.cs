using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _productRepository = productRepository;
            _fileService = fileService;
        }
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
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
                p=>p.Status == EntityStatus.Active,
                new string[] {nameof(Product.Translations),
                nameof(Product.CreatedBy)}
            );
            var productResponses = products.Adapt<List<ProductResponse>>();
            return productResponses;
        }
        public async Task<ProductResponse?> GetProduct(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOne(filter,
            new string[] {
                nameof(Product.Translations),
                nameof(Product.CreatedBy)});
            if (product == null) return null;
            return product.Adapt<ProductResponse>();
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _productRepository.GetOne(p => p.Id == id);
            if (product == null)
                return false;
            _fileService.Delete(product.MainImage);
            return await _productRepository.DeleteAsync(product);
        }
        public async Task<bool> UpdateProduct(int id, ProductUpdateRequest request)
        {
            var productDb = await _productRepository.GetOne(
                p => p.Id == id,
                new string[] { nameof(Product.Translations) });

            if (productDb == null)
                return false;

            var oldImage = productDb.MainImage;
            request.Adapt(productDb);

            if (request.Translations != null)
            {
                foreach (var translationRequest in request.Translations)
                {
                    var existing = productDb.Translations.FirstOrDefault(t => t.Language == translationRequest.Language);
                    if (existing != null)
                    {
                        if (translationRequest.Name != null)
                        {
                            existing.Name = translationRequest.Name;
                        }
                        if (translationRequest.Description != null)
                        {
                            existing.Description = translationRequest.Description;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (request.MainImage != null)
            {
                _fileService.Delete(oldImage);
                productDb.MainImage = await _fileService.UploadAsync(request.MainImage);
            }
            return await _productRepository.UpdateAsync(productDb);
        }
        public async Task<bool> ToggleStatus(int id)
        {
            var product = await _productRepository.GetOne(p => p.Id == id);
            if (product == null)
                return false;
            product.Status = product.Status == EntityStatus.Active ? EntityStatus.Inactive : EntityStatus.Active;
            return await _productRepository.UpdateAsync(product);
        }
    }
}