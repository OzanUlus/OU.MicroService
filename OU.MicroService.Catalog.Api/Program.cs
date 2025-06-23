using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api;
using OU.MicroService.Catalog.Api.Features.Categories;
using OU.MicroService.Catalog.Api.Options;
using OU.MicroService.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();

app.AddCategoryGroupEndpointExt();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();


