using Duende.IdentityModel.Client;
using OU.Microservice.Web.Options;
using OU.Microservice.Web.Services;

namespace OU.Microservice.Web.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption, HttpClient client, ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> SignIn(SignInViewModel signInViewModel)
        {
            
            
                var tokenResponse = await GetAccessToken(signInViewModel);
                if (tokenResponse.IsError)
                {
                   
                    return ServiceResult<string>.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
                }
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
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password,
                
            });
           
            return tokenResponse;
        }
    }
}



