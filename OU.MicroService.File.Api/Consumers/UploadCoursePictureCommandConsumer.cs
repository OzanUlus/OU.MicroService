using MassTransit;
using Microsoft.Extensions.FileProviders;
using OU.Microservice.Bus.Commands;

namespace OU.MicroService.File.Api.Consumers
{
    public class UploadCoursePictureCommandConsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();

            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}"; // .jpg
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);

            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.picture);




        }
    }
}
