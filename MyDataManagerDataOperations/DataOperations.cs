using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataManagerWinForms;
using MyDataModels;
using SixLinksDataService;

namespace MyDataManagerDataOperations
{
    public class DataOperations : IDataOperations
    {
        public static IConfigurationRoot _configuration;
        public static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;
        private readonly ISixLinksData _sixLinksData;

        public DataOperations(ISixLinksData sixLinksData)
        {
            //BuildOptions();
            _sixLinksData = sixLinksData;

        }

        public static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDataManagerData"));
        }

        //Actor Methods
        public async Task<List<Actor>> GetActors()
        {
            return await _sixLinksData.GetActors();
        }
        public async Task<Actor> GetActorById(int id)
        {
            return await _sixLinksData.GetActorById(id);
        }

        public async Task<List<Actor>> GetActorsFromDB(Movie selectedMovie)
        {
            return await _sixLinksData.GetActorsFromDB(selectedMovie);
        }
        public async Task<bool> CheckExistingActor(string firstName, string lastName)
        {
            return await _sixLinksData.CheckExistingActor(firstName, lastName);
        }
        public async Task<bool> CheckExistingActor(int id)
        {
            return await _sixLinksData.CheckExistingActor(id);
        }
        public async Task AddNewActor(Actor actor)
        {
            await _sixLinksData.AddNewActor(actor);
        }
        public async Task UpdateActor(int actorId, string firstName, string lastName)
        {
            await _sixLinksData.UpdateActor(actorId, firstName, lastName);
        }
        public async Task DeleteActor(Actor selectedActor)
        {
            await _sixLinksData.DeleteActor(selectedActor);
        }

        //Movie Methods
        public async Task<List<Movie>> GetMovies()
        {
            return await _sixLinksData.GetMovies();
        }
        public async Task<Movie> GetMovieById(int id)
        {
            return await _sixLinksData.GetMovieById(id);
        }
        public async Task<List<Movie>> GetMoviesFromDB(Actor selectedActor)
        {
            return await _sixLinksData.GetMoviesFromDB(selectedActor);
        }
        public async Task AddNewMovie(Movie movie)
        {
            await _sixLinksData.AddNewMovie(movie);
        }
        public async Task DeleteMovie(Movie selectedMovie)
        {
            await _sixLinksData.DeleteMovie(selectedMovie);
        }
        //public async Task<bool> AddMovieToDB(string title, int year)
        //{
        //    return await _sixLinksData.AddMovieToDB(title, year);
        //}

        public async Task<bool> CheckExistingMovie(int id)
        {
            return await _sixLinksData.CheckExistingMovie(id);
        }
        public async Task updateMovie(int movieId, string movieTitle, int movieYear)
        {
            await _sixLinksData.updateMovie(movieId, movieTitle, movieYear);
        }
        //Movie-Actor Methods

        public async Task<List<Movie_Actor>> GetMovie_Actors()
        {
            return await _sixLinksData.GetMovie_Actors();
        }

        public async Task InitialDatabaseLoad()
        {
            //using (var db = new DataDbContext(_optionsBuilder.Options))
            //{
            //    if (!db.Movies.Any() && !db.Actors.Any())
            //    {
            //        var di = new DataImporter();
            //        Task.Run(async () => await di.GetInitialData());
            //        Thread.Sleep(60000);
            //    }
            //}
        }
    }
}