using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PRN221_Cinema.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PRN221_Cinema.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly CinemaDBContext _context;
        [BindProperty]
        public List<Movie> movies { get; set; }
        [BindProperty]
        public List<Genre> genres { get; set; }
        [BindProperty]
        public int genreId { get; set; }
        [BindProperty]
        public string searchStr { get; set; }

        public IndexModel(ILogger<IndexModel> logger, CinemaDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(int? genreId, string? searchStr)
        {
            IQueryable<Movie> query = _context.Movies.Include(x => x.Genre);

            if (genreId != null)
            {
                query = query.Where(m => m.GenreId == genreId);
            }

            if (!string.IsNullOrEmpty(searchStr))
            {
                //query = query.Where(m => EF.Functions.Like(m.Title, $"%{searchStr}%"));
                query = query.Where(m => m.Title.Contains(searchStr));
            }
            //Process calculate averrage point each film
            var ratingQuery = _context.Rates
                .GroupBy(r => r.MovieId)
                .Select(g => new { MovieId = g.Key, RatingPoint = g.Average(r => r.NumericRating) });

            query = query
                .GroupJoin(ratingQuery, m => m.MovieId, r => r.MovieId, (m, r) => new { Movie = m, Rating = r })
                .SelectMany(x => x.Rating.DefaultIfEmpty(), (x, r) => new Movie
                {
                    MovieId = x.Movie.MovieId,
                    Description = x.Movie.Description,
                    Genre = x.Movie.Genre,
                    GenreId = x.Movie.GenreId,
                    Image = x.Movie.Image,
                    Rates = x.Movie.Rates,
                    Title = x.Movie.Title,
                    Year = x.Movie.Year,
                    RatingPoint = r.RatingPoint
                });
            movies = query.ToList();
            genres = _context.Genres.ToList();
        }
    }
}