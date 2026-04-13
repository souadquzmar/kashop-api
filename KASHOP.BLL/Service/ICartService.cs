using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;

namespace KASHOP.BLL.Service
{
    public interface ICartService
    {
        Task<bool> AddToCart(AddToCartRequest request, string UserId);
    }
}