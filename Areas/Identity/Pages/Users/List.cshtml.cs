using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace intexii.Areas.Identity.Pages.Users
{
    [Authorize(Roles="Admin")]
    public class ListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
