using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN221_Cinema.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Rates = new HashSet<Rate>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        [NotMapped]

        public double? RatingPoint { get; set; }
        [NotMapped]

        public string FormattedRatingPoint => RatingPoint.HasValue ? Math.Round(RatingPoint.Value, 2).ToString("0.00") : "Chưa có đánh giá";
    }
}
