using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OU.Microservice.Web.Pages.Instructor.ViewModel;
using OU.Microservice.Web.Services;

namespace OU.Microservice.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; } = null!;
        public async Task OnGet()
        {
            var result = await catalogService.GetCoursesByUserId();

            if (result.IsFail)
            {
                //TODO : redirect error page
            }

            CourseViewModels = result.Data!;
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteAsync(id);
            if (result.IsFail)
            {
                //TODO : redirect error page
            }

            return RedirectToPage();
        }
    }
}
