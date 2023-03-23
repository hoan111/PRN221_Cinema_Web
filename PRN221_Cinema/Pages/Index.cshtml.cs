using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            movies = query.ToList();
            genres = _context.Genres.ToList();
        }
    }
}