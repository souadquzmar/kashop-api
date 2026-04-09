using KASHOP.BLL.Mapping;
using KASHOP.BLL.Service;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.DAL.utils;
using KASHOP.PL.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//db
builder.Services.AddDatabaseServices(builder.Configuration);
//identity
builder.Services.AddIdentityServices();
//jwt auth
builder.Services.AddJwtAuthentication(builder.Configuration);
//localization
builder.Services.AddLocalizationServices();
//scoped
builder.Services.AddApplicationServices();
//cors policy
builder.Services.AddCorsPolicyServices();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();



MapsterConfig.MapsterConfigRegister();
var app = builder.Build();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(CorsPolicyExtensions.PolicyName);
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeders = services.GetServices<ISeedData>();
    foreach (var seeder in seeders)
    {
        await seeder.DataSeed();
    }
}
app.Run();
