using Application.Activities;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services,
        IConfiguration config) 
        //IserviceCollection untuk connect dengan service, Iconfiguration untuk connect dengan appsetting n development.json
        //this digunakan untuk menentukan parameter yang menjadi method, sehingga tidak perlu dipanggil ulang saat digunakan
        //dalam konteks ini IServiceCollection diberi this sehingga menjadi method, dan param dari public hanya IConfiguration
        {    
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddDbContext<DataContext>(opt=>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }); //Connection dengan Database + setting di middleware + harus ditambahkan di appsetting.development.json

        services.AddCors(opt=>
        {
        opt.AddPolicy("CorsPolicy",policy =>{
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });
         }); //connection dengan react menggunakan corspolicy

        services.AddMediatR(typeof(List.Handler)); //Connection dengan Mediator
        services.AddAutoMapper(typeof(MappingProfiles).Assembly); //Connection dengan AutoMapping
        //Assembly digunakan menemukan data pada MappingProfiles
        services.AddFluentValidationAutoValidation(); //Connection dengan Fluent Validation
        services.AddValidatorsFromAssemblyContaining<Create>();
        services.AddHttpContextAccessor();//Connection dengan infrastructure folder
        services.AddScoped<IUserAccessor,UserAccessor>();//Untuk menginject application handler

        return services; 

        }
    }
}