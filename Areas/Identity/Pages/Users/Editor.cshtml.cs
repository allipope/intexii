using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace intexii.Areas.Identity.Pages.Users
{
    public class EditorModel : PageModel
    {
        [Authorize(Roles="Admin")]
        public void OnGet()
        {
        }
    }
}
