using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.Identity.Client;

namespace KASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
            .Map(dest=>dest.User,source=>source.CreatedBy)
            .Map(dest=>dest.Name,source=>source.Translations.Where(t=>t.Language == CultureInfo.CurrentCulture.Name).Select(t=>t.Name).FirstOrDefault());
        }
    }
}