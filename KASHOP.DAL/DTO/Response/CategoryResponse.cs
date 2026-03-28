using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int Id {get; set;}
        public string User{get; set;}
        //public List<CategoryTranslationResponse> Translations {get; set;}
        public string Name{get; set;}
    }
}