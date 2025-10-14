using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OU.Microservice.Web.Pages.Auth.SignUp;

namespace OU.Microservice.Web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
       [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
           var result = await signUpService.CreateAccount(SignUpViewModel);
            if (result.IsFail)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
