using AutoMapper.Configuration.Annotations;
using PRN231_Cinema_Web_API.Models;

namespace PRN231_Cinema_Web_API.DAO
{
    public class MovieDAO
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public int? Year { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        [Ignore]
        public string? Genre { get; set; }
        [Ignore]
        public string? Rates { get; set; }
    }
}
