using Microsoft.AspNetCore.Authentication.Cookies;
using OU.Microservice.Web.DelegateHandlers;
using OU.Microservice.Web.Extensions;
using OU.Microservice.Web.Options;
using OU.Microservice.Web.Pages.Auth.SignIn;
using OU.Microservice.Web.Pages.Auth.SignUp;
using OU.Microservice.Web.Services;
using OU.Microservice.Web.Services.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptionsExt();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddHttpClient<TokenService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRefitClient<ICatologRefitService>().ConfigureHttpClient(cofigure =>
{
    var gatewayOption = builder.Configuration.GetSection(nameof(GatewayOption)).Get<GatewayOption>();
    cofigure.BaseAddress = new Uri(gatewayOption!.BaseAddress);

}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
  .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();
   

builder.Services.AddAuthentication(configureOption =>
{
    configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.Cookie.Name = "MicroserviceAuthCookie";
    opt.AccessDeniedPath = "/Auth/AccessDenied";

});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
