namespace OU.Microservice.Shared.Services
{
    public interface IIdentityService
    {
        public Guid UserId { get; }
        public string UserName { get; }
        List<string> Roles { get; }
    }
}
