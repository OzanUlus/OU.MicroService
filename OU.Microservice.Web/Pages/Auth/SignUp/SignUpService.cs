using Duende.IdentityModel.Client;
using OU.Microservice.Web.Options;
using OU.Microservice.Web.Services;

namespace OU.Microservice.Web.Pages.Auth.SignUp
{
    public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
    {
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var address = $"{identityOption.Admin.Address}/users";
            client.SetBearerToken(token);
            var userCreateRequest = CreateUserCreateRequest(model);
            var response = await client.PostAsJsonAsync(address, userCreateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                logger.LogError(error);
                return ServiceResult.Error("Create account failed. Please try again later.");
            }
            return ServiceResult.Success();
        }

        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                model.UserName,
                Enabled: true,
                model.FirstName,
                model.LastName,
                model.Email,
                Credentials: [new Credential("password", model.Password, Temporary: false)]
                );
        }
        public async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Admin.Address,
                Policy = { RequireHttps = false }
            };


            client.BaseAddress = new Uri(identityOption.Admin.Address);


            var discoveryResponse = await client.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,

            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken!;
        }
    }
}
