

namespace OU.Microservice.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        public string UserName => "Ozan" ;

        public Guid UserId => throw new NotImplementedException();

        public List<string> Roles => throw new NotImplementedException();
    }
}
