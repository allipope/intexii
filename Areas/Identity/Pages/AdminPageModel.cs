// namespace intexii.Areas.Identity.Pages
// {
//     public class AdminPageModel
//     {
//     }
// }

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Advanced.Pages {
    [Authorize(Roles="Admin")]
    public class AdminPageModel: PageModel {
    }
}
