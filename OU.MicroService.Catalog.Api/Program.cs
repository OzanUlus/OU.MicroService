using OU.Microservice.Bus;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Catalog.Api;
using OU.MicroService.Catalog.Api.Features.Categories;
using OU.MicroService.Catalog.Api.Features.Courses;
using OU.MicroService.Catalog.Api.Options;
using OU.MicroService.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);


var app = builder.Build();
app.AddSeedDataExt().ContinueWith(x =>
{
    if (x.IsFaulted)
    {
        Console.WriteLine(x.Exception?.Message);
    }
    else
    {
        Console.WriteLine("Seed data has been succesfully.");
    }
});
app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();



app.Run();


