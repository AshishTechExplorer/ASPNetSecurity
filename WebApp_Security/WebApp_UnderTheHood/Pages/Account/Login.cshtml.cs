using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        //private readonly SignInManager<User> signInManager;

        public LoginModel()
        {
            // this.signInManager = signInManager;
        }

        [BindProperty]
        public CredentialViewModel Credential { get; set; } = new CredentialViewModel();

        [BindProperty]
        public IEnumerable<AuthenticationScheme> ExternalLoginProviders { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();


            if (Credential.Email.Equals("admin") && Credential.Password.Equals("admin"))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@gmail.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Role", "Manager"),
                    new Claim("EmploymentDate", "2023-05-01"),
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal cp = new ClaimsPrincipal(identity);
                // IF Cookie is persistant even though we close the browser, we dont need to login even after closing it . LIfe time is set at program.cs "ExpireTimeSpan"
                var isPersistantCooke = new AuthenticationProperties()
                {
                    IsPersistent = Credential.RememberMe
                };

                await HttpContext.SignInAsync("MyCookieAuth", cp, isPersistantCooke);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class CredentialViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
