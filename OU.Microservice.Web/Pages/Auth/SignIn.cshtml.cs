using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OU.Microservice.Web.Pages.Auth.SignIn;
using OU.Microservice.Web.Pages.Auth.SignUp;

namespace OU.Microservice.Web.Pages.Auth
{
    public class SignInModel : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;
        public void OnGet()
        {
        }
    }
}
