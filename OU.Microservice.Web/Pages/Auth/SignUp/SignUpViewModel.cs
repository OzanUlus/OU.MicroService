using System.ComponentModel.DataAnnotations;

namespace OU.Microservice.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; init; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; init; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; init; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Ivalid email address")]
        public string Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "PasswordConfirm is required")]
        [Compare(nameof(Password), ErrorMessage = "The password dont match")]
        public string PasswordConfirm { get; init; }


        public static SignUpViewModel Empty => new()
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            PasswordConfirm = string.Empty
        };

        public static SignUpViewModel GetExampleModel => new()
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe",
            Email = "johnDoe@mail.com",
            Password = "P@ssw0rd",
            PasswordConfirm = "P@ssw0rd"

        };
          
    }

}
      

