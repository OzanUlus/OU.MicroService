using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OU.Microservice.Web.Options;
using OU.Microservice.Web.Services;
using System.Security.Claims;

namespace OU.Microservice.Web.Pages.Auth.SignIn
{
    public class SignInService(
        IHttpContextAccessor contextAccessor,
        TokenService tokenService,
        IdentityOption identityOption,
        HttpClient client,
        ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {

            var tokenResponse = await GetAccessToken(signInViewModel);
            if (tokenResponse.IsError)
            {

                return ServiceResult<string>.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }

            var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken!);
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await contextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);


            return ServiceResult.Success();


        }
        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };


            client.BaseAddress = new Uri(identityOption.Address);


            var discoveryResponse = await client.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password,
               

            });

            return tokenResponse;
        }
    }
}



