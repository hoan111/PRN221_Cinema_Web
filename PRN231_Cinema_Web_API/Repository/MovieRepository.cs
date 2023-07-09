using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN231_Cinema_Web_API.Config;
using PRN231_Cinema_Web_API.DAO;
using PRN231_Cinema_Web_API.Models;
using System.Runtime.ConstrainedExecution;

namespace PRN231_Cinema_Web_API.Repository
{
    public class MovieRepository
    {
        private static readonly MyDbContext myDbContext = new MyDbContext();

        public List<MovieDAO> GetAllMovies(int pageNumber, int pageSize)
        {
            int skipCount = (pageNumber - 1) * pageSize;

            List<Movie> movies = myDbContext.Movies
                .OrderBy(b => b.Year)
                .Skip(skipCount)
                .Take(pageSize)
                .Include(g => g.Genre)
                .Include(r => r.Rates)
                .ToList();
            List<MovieDAO> MovieDAO = new List<MovieDAO>();
            var mapper = AutoMapperConfig.InitializeAutomapper<Movie, MovieDAO>();
            foreach (Movie movie in movies)
            {
                MovieDAO daoEnt = mapper.Map<MovieDAO>(movie);
                //Xử lí những trường custom
                daoEnt.Genre = movie.Genre.Description;
                daoEnt.Rates = movie.Rates.Average(r => r.NumericRating).GetValueOrDefault(0.0).ToString("N2");
                MovieDAO.Add(daoEnt);
            }
            return MovieDAO;
        }
    }
}
