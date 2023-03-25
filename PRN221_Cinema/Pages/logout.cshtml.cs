using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_Cinema.Pages
{
    public class logoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("FullName")))
            {
                HttpContext.Session.Remove("FullName");
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Clear();
            }
            Response.Redirect("/");
        }
    }
}
