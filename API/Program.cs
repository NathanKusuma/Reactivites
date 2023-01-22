using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAplicationServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionsMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy"); //Middleware untuk connect dengan react (nama dalam tanda kurung harus sesuai dengan nama pada service diatas)

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();//Using berfungsi apabila dalam scope sudah tidak digunakan akan dihapus dari memory
var services = scope.ServiceProvider;

try
{
    var context= services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"An Error occured during migration");
} //Middleware untuk connect dengan database

app.Run();
