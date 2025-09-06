namespace OU.Microservice.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid courseId, Byte[] picture)
    {
    }
}
