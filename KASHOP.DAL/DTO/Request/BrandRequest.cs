using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace KASHOP.DAL.DTO.Request
{
    public class BrandRequest
    {
        public string Name {get; set;}
        public IFormFile Logo {get; set;}
        public List<BrandTranslation> Translations {get; set;}
    }
}