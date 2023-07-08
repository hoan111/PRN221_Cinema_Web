using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using PRN221_Cinema.DAO;
using PRN221_Cinema.Models;

namespace PRN221_Cinema.Pages
{

    public class commentModel : PageModel
    {
        private readonly CinemaDBContext _context;
        [BindProperty]
        public CommentDAO commentDAO { get; set; }

        public commentModel(CinemaDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
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
