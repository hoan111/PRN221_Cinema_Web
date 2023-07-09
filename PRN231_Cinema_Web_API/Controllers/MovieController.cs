using Microsoft.AspNetCore.Mvc;
using PRN231_Cinema_Web_API.Repository;

namespace PRN231_Cinema_Web_API.Controllers
{
    [Route("/api/movive")]
    public class MovieController : ControllerBase
    {
        private static MovieRepository? movieRepository;
        public MovieController()
        {
            movieRepository = new MovieRepository();
        }
        [Route("/GetAll")]
        [HttpGet]
        public IActionResult GetAllMovies(int PageSize, int PageNumber)
        {
            return Ok(movieRepository.GetAllMovies(PageNumber, PageSize));
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetMovie(int id)
        {
            return Ok(movieRepository.GetMovie(id));
        }
    }
}
