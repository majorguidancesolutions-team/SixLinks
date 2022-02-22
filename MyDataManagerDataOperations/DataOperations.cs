using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataManagerWinForms;
using MyDataModels;

namespace MyDataManagerDataOperations
{
    public class DataOperations
    {

        public static IConfigurationRoot _configuration;
        public static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;

        public DataOperations()
        {
            BuildOptions();
        }

        public static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDataManagerData"));
        }

        public List<Movie> GetMovies()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                return db.Movies.Include(x => x.MovieActors).OrderBy(x => x.Title).ToList();
            }
        }

        public List<Actor> GetActors()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                return db.Actors.Include(x => x.ActorMovies).OrderBy(x => x.FirstName).ToList();
            }
        }

        public List<Movie_Actor> GetMovie_Actors()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                return db.Movies_Actors.OrderBy(x => x.Id).ToList();
            }
        }

        public void InitialDatabaseLoad()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                if (!db.Movies.Any() && !db.Actors.Any())
                {
                    var di = new DataImporter();
                    Task.Run(async () => await di.GetInitialData());
                    Thread.Sleep(60000);
                }
            }
        }

        public List<Actor> GetActorsFromDB(Movie selectedMovie)
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var movieData = db.Movies
                                .Include(x => x.MovieActors)
                                .ThenInclude(y => y.Actor)
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Actors = x.MovieActors.Select(y => y.Actor)
                                })
                                .FirstOrDefault(x => x.Id == selectedMovie.Id);

                return movieData?.Actors?.ToList() ?? new List<Actor>();
            }
        }

        public List<Movie> GetMoviesFromDB(Actor selectedActor)
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var actorData = db.Actors
                                .Include(x => x.ActorMovies)
                                .ThenInclude(y => y.Movie)
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    Movies = x.ActorMovies.Select(y => y.Movie)
                                })
                                .FirstOrDefault(x => x.Id == selectedActor.Id);

                return actorData?.Movies?.ToList() ?? new List<Movie>();
            }
        }

        public void DeleteMovie(Movie selectedMovie)
        {
            var deleteID = selectedMovie.Id;
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var movieToRemove = db.Movies.SingleOrDefault(x => x.Id == deleteID);
                if (movieToRemove != null)
                {
                    db.Movies.Remove(movieToRemove);
                    db.SaveChanges();
                }
            }
        }
        public void DeleteActor(Actor selectedActor)
        {
            var deleteID = selectedActor.Id;
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var actorToRemove = db.Actors.SingleOrDefault(x => x.Id == deleteID);
                if (actorToRemove != null)
                {
                    db.Actors.Remove(actorToRemove);
                    db.SaveChanges();
                }
            }
        }
    }
}