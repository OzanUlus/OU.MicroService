
using Duende.IdentityModel.Client;
using OU.Microservice.Web.Services;

namespace OU.Microservice.Web.DelegateHandlers
{
    public class ClientAuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor, TokenService tokenService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);


            if (httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);

            var tokenResponse = await tokenService.GetClientAccessToken();
            if (tokenResponse.IsError)
            {
                throw new UnauthorizedAccessException("Failed to get access token.");
            }
            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
