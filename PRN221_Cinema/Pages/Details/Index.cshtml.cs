using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Cinema.DAO;
using PRN221_Cinema.Models;

namespace PRN221_Cinema.Pages.Details
{
    public class IndexModel : PageModel
    {
        private readonly CinemaDBContext _context;
        [BindProperty]
        public Movie _movie { get; set; }
        [BindProperty]
        public int id { get; set; }
        [BindProperty]
        public List<Rate> rates { get; set; }
        [BindProperty]
        public Rate _rate { get; set; }
        [BindProperty]
        public CommentDAO commentDAO { get; set; }
        public IndexModel(CinemaDBContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            int? persionId = HttpContext.Session.GetInt32("UserId");
            if (id != null)
            {
                IQueryable<Movie> query = _context.Movies.Include(x => x.Genre);

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
                _movie = query.Where(m => m.MovieId == id).FirstOrDefault();

                rates = _context.Rates.Include(p => p.Person).Where(r => r.MovieId == id).ToList();

                _rate = _context.Rates.Where(r => r.MovieId == id && r.PersonId == persionId).FirstOrDefault();
            }
        }

        public void OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid && userId != null)
            {
                Rate rate = null;
                rate = _context.Rates.Where(r => r.MovieId == commentDAO.MovieId && r.PersonId == userId).FirstOrDefault();
                if (rate != null)
                {
                    rate.NumericRating = commentDAO.RatePoint;
                    rate.Comment = commentDAO.comment;
                    _context.Rates.Update(rate);
                    _context.SaveChanges();
                }
                else
                {
                    rate = new Rate();
                    Movie movie = _context.Movies.Where(m => m.MovieId == commentDAO.MovieId).FirstOrDefault();
                    rate.Movie = movie;
                    rate.MovieId = commentDAO.MovieId;
                    rate.NumericRating = commentDAO.RatePoint;
                    rate.Comment = commentDAO.comment;
                    rate.Time = DateTime.Now;
                    rate.Person = _context.Persons.Where(m => m.PersonId == userId).FirstOrDefault();

                    _context.Rates.Add(rate);
                    _context.SaveChanges();
                }
                Response.Redirect($"/details?id={commentDAO.MovieId}");
            }
        }
    }
}
