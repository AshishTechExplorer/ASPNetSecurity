using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Shared
{
    [Authorize("HRManager")]
    public class HrManagerPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
