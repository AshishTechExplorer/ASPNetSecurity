using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Shared
{
    [Authorize(Policy = "MustBelongToHRDepartment")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
