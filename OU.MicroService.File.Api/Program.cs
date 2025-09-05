using Microsoft.Extensions.FileProviders;
using OU.Microservice.Bus;
using OU.Microservice.Shared.Extensions;
using OU.MicroService.File.Api;
using OU.MicroService.File.Api.Features.File;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddVersioningExt();

var app = builder.Build();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.AddFileGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();

app.Run();


