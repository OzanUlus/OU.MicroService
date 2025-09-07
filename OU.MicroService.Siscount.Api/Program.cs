using OU.Microservice.Bus;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Discount.Api;
using OU.MicroService.Discount.Api.Features.Courses;
using OU.MicroService.Discount.Api.Options;
using OU.MicroService.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.MasstransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddVersioningExt();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();

app.Run();


