using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;

namespace KASHOP.BLL.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task<bool> AddToCart(AddToCartRequest request, string UserId)
        {
            var product = await _productRepository.GetOne(p => p.Id == request.ProductId);
            if (product == null)
                return false;
            var ExistingItem = await _cartRepository.GetOne(
                c => c.ProductId == request.ProductId && c.UserId == UserId
            );

            var currentCount = ExistingItem?.Count ?? 0;
            var newCount = currentCount + request.Count;
            if (newCount > product.Quantity)
                return false;
            if (ExistingItem != null)
            {
                ExistingItem.Count += request.Count;
                await _cartRepository.UpdateAsync(ExistingItem);
            }
            else
            {
                var cartItem = request.Adapt<Cart>();
                cartItem.UserId = UserId;
                await _cartRepository.CreateAsync(cartItem);
            }
            return true;
        }
    }
}