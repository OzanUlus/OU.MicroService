using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OU.Microservice.Web.Pages.Auth.SignIn;
using OU.Microservice.Web.Pages.Auth.SignUp;

namespace OU.Microservice.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInService.AuthenticateAsync(SignInViewModel);
            if (result.IsFail)
            {
                ModelState.AddModelError(string.Empty, result.Fail.Title);
                ModelState.AddModelError(string.Empty, result.Fail.Detail);
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
