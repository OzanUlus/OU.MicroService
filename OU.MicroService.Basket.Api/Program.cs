using OU.Microservice.Bus;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.Basket.Api;
using OU.MicroService.Basket.Api.Features.Basket;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(opt => {

    opt.Configuration = builder.Configuration.GetConnectionString("Redis");

});
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddVersioningExt();
builder.Services.AddScoped<BasketService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthorization();
app.UseAuthentication();

app.Run();


