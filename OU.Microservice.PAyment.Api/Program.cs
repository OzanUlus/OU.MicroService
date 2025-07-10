using Microsoft.EntityFrameworkCore;
using OU.Microservice.Payment.Api.Features;
using OU.Microservice.Payment.Api.Repositories;
using OU.Microservice.PAyment.Api;
using OU.Microservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("payment-in-memory-db");
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());



app.Run();

