using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Cinema.Models;

namespace PRN221_Cinema.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly CinemaDBContext _context;
        [BindProperty]
        public Person person { get; set; }
        [BindProperty]
        public string msg { get; set; }
        [BindProperty]
        public bool isRegisterSuccess { get; set; }

        public IndexModel(CinemaDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ModelState.Remove("msg");
            if (ModelState.IsValid)
            {
                _context.Persons.Add(person);
                msg = "Đăng kí tài khoản thành công";
                isRegisterSuccess = true;
                _context.SaveChanges();
            }
            else
            {
                msg = "Đăng kí tài khoản thất bại";
                isRegisterSuccess = false;
            }
        }
    }
}
