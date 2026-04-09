using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.PL.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>{
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));});
            return Services;
        }
    }
}