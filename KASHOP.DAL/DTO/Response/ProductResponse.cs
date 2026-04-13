using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace KASHOP.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id {get; set;}
        public string User {get; set;}
        public string Name {get; set;}
        public string MainImage {get; set;}
        public decimal Price {get; set;}
        public int Quantity {get; set;}
    }
}