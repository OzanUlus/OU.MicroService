using OU.Microservice.Web.Pages.Instructor.Dto;
using Refit;

namespace OU.Microservice.Web.Services.Refit
{
    public interface ICatologRefitService
    {
        //[Get("/api/v1/courses")]
        //Task<ApiResponse<List<CourseDto>>> GetAllCourses();

        //[Get("/api/v1/courses/{id}")]
        //Task<ApiResponse<CourseDto>> GetCourse(Guid id);


        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();


        //[Get("/api/v1/courses/user/{userId}")]
        //Task<ApiResponse<List<CourseDto>>> GetCoursesByUserId(Guid UserId);



        [Post("/api/v1/courses")]
        Task<ApiResponse<ServiceResult>> CreateCourseAsync(CreateCourseRequest request);


        [Put("/api/v1/courses")]
        Task<ApiResponse<ServiceResult>> UpdateCourseAsync(UpdateCourseRequest request);


        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<ServiceResult>> DeleteCourseAsync(Guid id);
    }
}
