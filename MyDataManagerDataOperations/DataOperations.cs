using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataManagerWinForms;
using MyDataModels;
using SixLinksDataService;

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

        public async Task<List<Movie>> GetMovies()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var m = new SixLinksData(db);
                return await m.GetMovies();
            }
        }

        public async Task<List<Actor>> GetActors()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var x = new SixLinksData(db);
                return await x.GetActors();
            }
        }

        public async Task<List<Movie_Actor>> GetMovie_Actors()
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var ma = new SixLinksData(db);
                return await ma.GetMovie_Actors();
            }
        }

        public async Task InitialDatabaseLoad()
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

        public async Task<List<Actor>> GetActorsFromDB(Movie selectedMovie)
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                //var movieData = await db.Movies
                //                .Include(x => x.MovieActors)
                //                .ThenInclude(y => y.Actor)
                //                .Select(x => new
                //                {
                //                    Id = x.Id,
                //                    Title = x.Title,
                //                    Actors = x.MovieActors.Select(y => y.Actor)
                //                })
                //                .FirstOrDefaultAsync(x => x.Id == selectedMovie.Id);

                var x = new SixLinksData(db);
                return await x.GetActorsFromDB(selectedMovie);
            }
        }

        public async Task<List<Movie>> GetMoviesFromDB(Actor selectedActor)
        {
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var actorData = await db.Actors
                                .Include(x => x.ActorMovies)
                                .ThenInclude(y => y.Movie)
                                .Select(x => new
                                {
                                    Id = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    Movies = x.ActorMovies.Select(y => y.Movie)
                                })
                                .FirstOrDefaultAsync(x => x.Id == selectedActor.Id);

                return actorData?.Movies?.ToList() ?? new List<Movie>();
            }
        }

        public async Task DeleteMovie(Movie selectedMovie)
        {
            var deleteID = selectedMovie.Id;
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var movieToRemove = await db.Movies.SingleOrDefaultAsync(x => x.Id == deleteID);
                if (movieToRemove != null)
                {
                    db.Movies.Remove(movieToRemove);
                    db.SaveChanges();
                }
            }
        }
        public async Task DeleteActor(Actor selectedActor)
        {
            var deleteID = selectedActor.Id;
            using (var db = new DataDbContext(_optionsBuilder.Options))
            {
                var actorToRemove = await db.Actors.SingleOrDefaultAsync(x => x.Id == deleteID);
                if (actorToRemove != null)
                {
                    db.Actors.Remove(actorToRemove);
                    db.SaveChanges();
                }
            }
        }
        public async Task<bool> CheckExistingActor(string firstName, string lastName)
        {
            // check that the input is not in database
            using (var db = new DataDbContext(DataOperations._optionsBuilder.Options))
            {
                var existingActor = await db.Actors.FirstOrDefaultAsync(x => x.FirstName == firstName
                                                            && x.LastName == lastName);
                if (existingActor is null)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task UpdateActor(string actorId, string firstName, string lastName)
        {
            using (var db = new DataDbContext(DataOperations._optionsBuilder.Options))
            {
                var existingActor = await db.Actors.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(actorId));
                existingActor.FirstName = firstName;
                existingActor.LastName = lastName;
                db.SaveChanges();
            }
        }

        public async Task<bool> AddMovieToDB(string title, int year)
        {
            using (var db = new DataDbContext(DataOperations._optionsBuilder.Options))
            {
                var newMovie = new Movie();
                newMovie.Title = title;
                newMovie.Year = year;

                var existingMovie = await db.Movies.FirstOrDefaultAsync(x => x.Title == newMovie.Title
                                                            && x.Year == newMovie.Year);
                if (existingMovie is null)
                {
                    db.Add(newMovie);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task updateMovie(string movieId, string movieTitle, int movieYear)
        {
            using (var db = new DataDbContext(DataOperations._optionsBuilder.Options))
            {
                var existingMovie = await db.Movies.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(movieId));
                existingMovie.Title = movieTitle;
                existingMovie.Year = movieYear;
                db.SaveChanges();
            }
        }
    }
}