using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public List<CategoryTranslationResponse> Translations {get; set;}
    }
}