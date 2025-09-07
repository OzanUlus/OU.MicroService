namespace OU.Microservice.Bus.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid Userıd)
    {
    }
}
