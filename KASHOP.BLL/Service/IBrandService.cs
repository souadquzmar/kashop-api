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
    public interface IBrandService
    {
        Task<BrandResponse> CreateBrandAsync (BrandRequest request);
        Task<List<BrandResponse>> GetAllBrandsAsync ();
        Task<BrandResponse> GetBrandAsync (Expression<Func<Brand,bool>> filter);
        Task<bool> DeleteBrandAsync(int id);
    }
}