using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Cinema.Models
{
    public partial class Person
    {
        public Person()
        {
            Rates = new HashSet<Rate>();
        }

        public int PersonId { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
        public int? Type { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
