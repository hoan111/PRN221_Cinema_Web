using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Cinema.Models;
using System.Net.WebSockets;

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
                //chỉ update khi mail không tồn tại trong hệ thống
                if (!checkDuplicateMail(person))
                {
                    _context.Persons.Add(person);
                    msg = "Đăng kí tài khoản thành công";
                    isRegisterSuccess = true;
                    _context.SaveChanges();
                }
                else
                {
                    msg = "Email này đã tồn tại. Vui lòng lựa chọn email khác";
                    isRegisterSuccess = false;
                }
            }
            else
            {
                msg = "Đăng kí tài khoản thất bại";
                isRegisterSuccess = false;
            }
        }
        //Check trùng email. Trường hợp email đã tồn tại thì return true, ngược lại thì false
        private bool checkDuplicateMail(Person person)
        {
            var tmp = _context.Persons.Where(p => p.Email.Equals(person.Email)).FirstOrDefault();
            if (tmp != null)
            {
                return true;
            }
            return false;
        }
    }
}
