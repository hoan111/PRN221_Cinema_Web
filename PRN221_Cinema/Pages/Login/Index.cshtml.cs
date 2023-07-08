using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Cinema.DAO;
using PRN221_Cinema.Models;

namespace PRN221_Cinema.Pages.Login
{

    public class IndexModel : PageModel
    {
        private readonly CinemaDBContext _context;

        [BindProperty]
        public LoginDAO loginDAO { get; set; }
        [BindProperty]
        public string errMsg { get; set; }
        public IndexModel(CinemaDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                Response.Redirect("/");
            }
        }
        public void OnPost()
        {
            ModelState.Remove("errMsg");
            if (ModelState.IsValid)
            {
                Person person = _context.Persons.Where(p => p.Email.Equals(loginDAO.email) && p.Password.Equals(loginDAO.password)).FirstOrDefault();
                if (person != null)
                {
                    HttpContext.Session.SetString("FullName", person.Fullname);
                    HttpContext.Session.SetInt32("UserId", person.PersonId);
                    Response.Redirect("/");
                }
                else
                {
                    errMsg = "Thông tin đăng nhập không đúng";
                }
            }
        }
    }
}
