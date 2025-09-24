using Duende.IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using OU.Microservice.Shared.Options;

namespace OU.Microservice.Order.Application.Contracts.Refit
{
    public class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider, IHttpClientFactory clientFactory) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = serviceProvider.CreateScope())

                var identityOptions = scope.ServiceProvider.GetRequiredService<IdentityOption>();
            var clientSecretOptions = scope.ServiceProvider.GetRequiredService<ClientSecretOption>();

            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Address,
                Policy = { RequireHttps = false }
            };

            var client = clientFactory.CreateClient();

            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest, cancellationToken);
            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientSecretOptions.Id,
                ClientSecret = clientSecretOptions.Secret,

            }, cancellationToken);

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
