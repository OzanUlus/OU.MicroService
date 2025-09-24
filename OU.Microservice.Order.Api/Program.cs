using Microsoft.EntityFrameworkCore;
using OU.Microservice.Bus;
using OU.Microservice.Order.Application;
using OU.Microservice.Order.Application.Contracts.Refit;
using OU.Microservice.Order.Application.Contracts.Refit.PaymentService;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Application.Contracts.UnitOfWork;
using OU.Microservice.Order.Persistance;
using OU.Microservice.Order.Persistance.Repositories;
using OU.Microservice.Order.Persistance.UnitOfWork;
using OU.Microservice.Shared.Extensions;
using OU.Microservice.Shared.Options;
using OU.MicroService.Catalog.Api.Features.Courses;
using Refit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddVersioningExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

builder.Services.AddOptions<IdentityOption>()
    .BindConfiguration(nameof(IdentityOption))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IdentityOption>(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<IdentityOption>>().Value);

builder.Services.AddOptions<ClientSecretOption>()
    .BindConfiguration(nameof(ClientSecretOption))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<ClientSecretOption>(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ClientSecretOption>>().Value);

builder.Services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
{
    var addressUrlOption = builder.Configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();

    configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
    


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();


app.Run();

