using System.ComponentModel.DataAnnotations;

namespace PRN221_Cinema.DAO
{
    public class CommentDAO
    {
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        public string comment { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        public double RatePoint { get; set; }
        public int MovieId { get; set; }
    }
}
