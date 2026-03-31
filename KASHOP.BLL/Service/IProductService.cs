using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProduct(Expression<Func<Product,bool>> filter);
        Task<bool> DeleteProduct(int id);
    }
}