using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OU.Microservice.Web.Services;

namespace OU.Microservice.Web.DelegateHandlers
{
    public class AuthenticationHttpClientHandler(IHttpContextAccessor httpContextAccessor, TokenService tokenService) : DelegatingHandler
    {
        protected override async  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

            var user = httpContextAccessor.HttpContext.User;
            if(!user.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);

            var accesToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (string.IsNullOrEmpty(accesToken))
            {
                throw new UnauthorizedAccessException("No acces token found");
            }

            request.SetBearerToken(accesToken);

            var response = await base.SendAsync(request, cancellationToken);

            if(response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                return response;
            }

            var refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("No refresh token found");
            }

            var tokenResponse = await tokenService.GetTokensByRefreshToken(refreshToken);
            if (tokenResponse.IsError)
            {
                throw new UnauthorizedAccessException("Failed to refresh access token.");
            }


            request.SetBearerToken(tokenResponse.AccessToken!);




            return await base.SendAsync(request, cancellationToken);
        }
    }
}
