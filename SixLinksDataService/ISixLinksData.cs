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
		Task<List<Actor>> GetActorsFromDB(Movie selectedMovie);

		//Movies methods
		Task<List<Movie>> GetMovies();

		//Movie-Actor Methods
		Task<List<Movie_Actor>> GetMovie_Actors();
		Task InitialDatabaseLoad();
	}
}
