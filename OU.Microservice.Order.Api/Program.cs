using Microsoft.EntityFrameworkCore;
using OU.Microservice.Order.Application;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Application.Contracts.UnitOfWork;
using OU.Microservice.Order.Persistance;
using OU.Microservice.Order.Persistance.Repositories;
using OU.Microservice.Order.Persistance.UnitOfWork;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api.Features.Courses;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddVersioningExt();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());


app.Run();

