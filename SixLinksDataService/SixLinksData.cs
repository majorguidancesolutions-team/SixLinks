using DataLibrary;
using Microsoft.EntityFrameworkCore;
using MyDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixLinksDataService
{
    public class SixLinksData : ISixLinksData
    {
        private readonly DataDbContext _context;
        public SixLinksData(DataDbContext context)
        {
            _context = context;
        }

        //Methods for actors
        public async Task<List<Actor>> GetActors()
        {
            return await _context.Actors.Include(x => x.ActorMovies).OrderBy(x => x.FirstName).AsNoTracking().ToListAsync(); ;
        }

        public async Task<Actor> GetActorById(int id)
        {
            return await _context.Actors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> AddNewActor(Actor userActor)
        {
            var exists = await _context.Actors.FirstOrDefaultAsync(x => x.FirstName == userActor.FirstName && x.LastName == userActor.LastName);
            if (exists is not null)
            {
                return exists.Id;
            }
            await _context.Actors.AddAsync(userActor);
            await _context.SaveChangesAsync();
            return userActor.Id;
        }
        public async Task<List<Actor>> GetActorsFromDB(Movie selectedMovie)
        {
            var movieData = await _context.Movies
                .Include(x => x.MovieActors)
                .ThenInclude(y => y.Actor)
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    Actors = x.MovieActors.Select(y => y.Actor)
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == selectedMovie.Id);

            return movieData?.Actors?.ToList() ?? new List<Actor>();
        }
        public async Task DeleteActor(Actor selectedActor)
        {
            var deleteID = selectedActor.Id;

            var actorToRemove = await _context.Actors.SingleOrDefaultAsync(x => x.Id == deleteID);
            if (actorToRemove != null)
            {
                _context.Actors.Remove(actorToRemove);
                _context.SaveChanges();
            }
        }
        public async Task UpdateActor(int actorId, string firstName, string lastName)
        {

            var existingActor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(actorId));
            existingActor.FirstName = firstName;
            existingActor.LastName = lastName;
            _context.SaveChanges();

        }

        public async Task<bool> CheckExistingActor(string firstName, string lastName)
        {
            // check that the input is not in database
            var existingActor = await _context.Actors.AsNoTracking().FirstOrDefaultAsync(x => x.FirstName == firstName
                                                        && x.LastName == lastName);
            if (existingActor is null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CheckExistingActor(int id)
        {
            var existingActor = await _context.Actors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existingActor is null)
            {
                return true;
            }
            return false;
        }

        //Methods for Movies
        public async Task<List<Movie>> GetMovies()
        {
            return await _context.Movies.Include(x => x.MovieActors).OrderBy(x => x.Title).AsNoTracking().ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesFromDB(Actor selectedActor)
        {
            var actorData = await _context.Actors
                            .Include(x => x.ActorMovies)
                            .ThenInclude(y => y.Movie)
                            .Select(x => new
                            {
                                Id = x.Id,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Movies = x.ActorMovies.Select(y => y.Movie)
                            })
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == selectedActor.Id);

            return actorData?.Movies?.ToList() ?? new List<Movie>();
        }
        public async Task DeleteMovie(Movie selectedMovie)
        {
            var deleteID = selectedMovie.Id;

            var movieToRemove = await _context.Movies.SingleOrDefaultAsync(x => x.Id == deleteID);
            if (movieToRemove != null)
            {
                _context.Movies.Remove(movieToRemove);
                _context.SaveChanges();
            }

        }
        public async Task updateMovie(int movieId, string movieTitle, int movieYear)
        {

            var existingMovie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(movieId));
            existingMovie.Title = movieTitle;
            existingMovie.Year = movieYear;
            _context.SaveChanges();

        }
        //public async Task<bool> AddMovieToDB(string title, int year)
        //{
        //    {
        //        var newMovie = new Movie();
        //        newMovie.Title = title;
        //        newMovie.Year = year;

        //        var existingMovie = await _context.Movies.FirstOrDefaultAsync(x => x.Title == newMovie.Title
        //                                                    && x.Year == newMovie.Year);
        //        if (existingMovie is null)
        //        {
        //            _context.Add(newMovie);
        //            _context.SaveChanges();
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}


        //Methods for Movie-Actors
        public async Task<List<Movie_Actor>> GetMovie_Actors()
        {
            return await _context.Movies_Actors.OrderBy(x => x.Id).AsNoTracking().ToListAsync();
        }
        public async Task InitialDatabaseLoad()
        {
            //if (!_context.Movies.Any() && !_context.Actors.Any())
            //{
            //    var di = new DataImporter();
            //    Task.Run(async () => await di.GetInitialData());
            //    Thread.Sleep(60000);
            //}
        }
    }
}








