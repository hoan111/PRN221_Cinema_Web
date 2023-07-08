using System.ComponentModel.DataAnnotations;

namespace PRN221_Cinema.DAO
{
    public class CommentDAO
    {
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        public string comment { get; set; }
        [Required(ErrorMessage = "Đây là trường bắt buộc")]
        [Range(0, 10, ErrorMessage = "Điểm đánh giá phải từ 0 đến 10")]
        public double RatePoint { get; set; }
        public int MovieId { get; set; }
    }
}
