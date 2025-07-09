using Microsoft.EntityFrameworkCore;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Persistance;
using OU.Microservice.Order.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();

