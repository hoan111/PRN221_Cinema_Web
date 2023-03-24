using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PRN221_Cinema.DAO
{
    public class LoginDAO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
