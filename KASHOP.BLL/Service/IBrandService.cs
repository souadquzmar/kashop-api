using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;

namespace KASHOP.BLL.Service
{
    public interface IBrandService
    {
        Task<BrandResponse> CreateBrandAsync (BrandRequest request);
    }
}