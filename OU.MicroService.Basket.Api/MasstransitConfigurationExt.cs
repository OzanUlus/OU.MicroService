using MassTransit;
using OU.Microservice.Bus;
using OU.MicroService.Basket.Api.Consumers;

namespace OU.MicroService.Basket.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection MasstransitExt(this IServiceCollection services,
         IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });


                    //cfg.ConfigureEndpoints(ctx);

                    cfg.ReceiveEndpoint("basket-microservice.order-created.queue",
                       e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });
                });
            });


            return services;
        }
    }
}
