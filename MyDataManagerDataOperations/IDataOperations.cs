using MyDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataManagerDataOperations
{
    public interface IDataOperations
    {
		//Actors methods
		Task<List<Actor>> GetActors();
		Task<Actor> GetActorById(int id);
		Task AddNewActor(Actor actor);
		Task<List<Actor>> GetActorsFromDB(Movie selectedMovie);
		Task DeleteActor(Actor selectedActor);
		Task<bool> CheckExistingActor(string firstName, string lastName);
		Task<bool> CheckExistingActor(int id);
		Task UpdateActor(int actorId, string firstName, string lastName);


		//Movies methods
		Task<List<Movie>> GetMovies();
		Task<List<Movie>> GetMoviesFromDB(Actor selectedActor);
		Task DeleteMovie(Movie selectedMovie);
		//Task<bool> AddMovieToDB(string title, int year);
		Task updateMovie(int movieId, string movieTitle, int movieYear);

		//Movie-Actor Methods
		Task<List<Movie_Actor>> GetMovie_Actors();
		Task InitialDatabaseLoad();
	}
}
