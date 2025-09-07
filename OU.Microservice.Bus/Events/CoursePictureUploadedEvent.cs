namespace OU.Microservice.Bus.Events
{
    public record CoursePictureUploadedEvent(Guid CourseId, string ImageUrl)
    {
    }
}
