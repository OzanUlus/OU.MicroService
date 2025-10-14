using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OU.Microservice.Web.Pages.Auth.SignUp;

namespace OU.Microservice.Web.Pages.Auth
{
    public class SignUpModel : PageModel
    {
       [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;
        public void OnGet()
        {
        }
    }
}
