using MassTransit;
using OU.Microservice.Bus;
using OU.MicroService.Catalog.Api.Consumenrs;

namespace OU.MicroService.Catalog.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection MasstransitExt(this IServiceCollection services,
          IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CoursePictureUploadedEventconsumer>();
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });


                    //cfg.ConfigureEndpoints(ctx);

                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue",
                       e => { e.ConfigureConsumer<CoursePictureUploadedEventconsumer>(ctx); });
                });
            });


            return services;
        }
    }
}

