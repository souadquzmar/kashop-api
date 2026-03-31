using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Service
{
    public interface IFileService
    {
        Task<string?> UploadAsync(IFormFile file);
        void Delete(string fileName);
    }
}