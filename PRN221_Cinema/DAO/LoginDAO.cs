using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PRN221_Cinema.DAO
{
    public class LoginDAO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
