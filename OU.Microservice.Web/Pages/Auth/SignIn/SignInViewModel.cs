using OU.Microservice.Web.Pages.Auth.SignUp;
using System.ComponentModel.DataAnnotations;

namespace OU.Microservice.Web.Pages.Auth.SignIn
{
    public record SignInViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Ivalid email address")]
        public string Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        public static SignInViewModel Empty => new()
        {
            Email = string.Empty,
            Password = string.Empty,
        };

        public static SignInViewModel GetExampleModel => new()
        {
           
            Email = "ilkerozanulus@gmail.com",
            Password = "Password12*",
           

        };
    }
}
