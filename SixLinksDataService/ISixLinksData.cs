using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using MyDataModels;

namespace SixLinksDataService
{
	public interface ISixLinksData
	{
		//Actors methods
		Task<List<Actor>> GetActors();
		Task<Actor> GetActorById(int id);
		Task<int> AddNewActor(Actor userActor);
		Task<List<Actor>> GetActorsFromDB(Movie selectedMovie);
		Task DeleteActor(Actor selectedActor);
		Task<bool> CheckExistingActor(string firstName, string lastName);
		Task<bool> CheckExistingActor(int id);
		Task UpdateActor(int actorId, string firstName, string lastName);


		//Movies methods
		Task<List<Movie>> GetMovies();
		Task<Movie> GetMovieById(int id);
		Task<List<Movie>> GetMoviesFromDB(Actor selectedActor);
		Task<int> AddNewMovie(Movie userMovie);
		Task DeleteMovie(Movie selectedMovie);
		//Task<bool> AddMovieToDB(string title, int year);
		Task<bool> CheckExistingMovie(int id);
		Task updateMovie(int movieId, string movieTitle, int movieYear);

		//Movie-Actor Methods
		Task<List<Movie_Actor>> GetMovie_Actors();
		Task InitialDatabaseLoad();
	}
}
