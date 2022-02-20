using DataLibrary;
using Microsoft.EntityFrameworkCore;
using MyDataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyDataManagerWinForms;
using static MyDataManagerWinForms.MainForm;

namespace MyDataManagerWinForms
{
	public class DataImporter
	{
		private static readonly HttpClient client = new HttpClient();

		public async Task GetInitialData()
		{
			using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
			{
				if (db.Movies.Any() || db.Actors.Any())
				{
					return;
				}

				var response = await client.GetAsync("https://imdb-api.com/en/API/Top250Movies/k_ttuui8u4");
				string json = await response?.Content?.ReadAsStringAsync() ?? string.Empty;

				if (string.IsNullOrEmpty(json))
				{
					return;
				}
				try
				{
					ImdbData data = JsonConvert.DeserializeObject<ImdbData>(json);
					List<Movie> ourMovies = new List<Movie>();
					List<Actor> ourActors = new List<Actor>();

					foreach (var item in data.items)
					{
						Movie movie = new Movie();
						movie.Title = item.title;
						movie.Year = 9999;
						movie.Crew = item.crew;

						if (int.TryParse(item.year, out int year))
						{
							movie.Year = year;
						}
						ourMovies.Add(movie);

						var crew = item.crew.Split(',');
						Debug.WriteLine(item.crew);

						foreach (var member in crew)
						{
							if (member.Contains("(dir.)"))
							{
								continue;
							}
							Debug.WriteLine(member);
							var name = member.Trim().Split(' ');

							if (name.Length == 0)
							{
								continue;
							}
							Actor actor = new Actor();
							actor.FirstName = name[0];
							actor.LastName = string.Empty;

							if (name.Length > 1)
							{
								StringBuilder sb = new StringBuilder();

								for (int i = 1; i < name.Length; i++)
								{
									if (sb.Length > 0)
									{
										sb.Append(' ');
									}
									sb.Append(name[i]);
								}
								actor.LastName = sb.ToString();
							}

							var existingActor = ourActors.FirstOrDefault(x => x.FirstName == actor.FirstName && x.LastName == actor.LastName);

							if (existingActor is null)
							{
								ourActors.Add(actor);
							}
						}
					}
					await AddInitialActors(ourActors);
					await AddInitialMovies(ourMovies);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private async Task AddInitialActors(List<Actor> actorsList)
		{
			using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
			{
				await db.Actors.AddRangeAsync(actorsList);
				await db.SaveChangesAsync();
			}
		}

		private async Task AddInitialMovies(List<Movie> movieList)
		{
			using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
			{
				var actors = await db.Actors.AsNoTracking().ToListAsync();

				foreach (var movie in movieList)
				{
					var crew = movie.Crew.Split(',');

					foreach (var member in crew)
					{
						if (member.Contains("(dir.)"))
						{
							continue;
						}

						var name = member.Trim().Split(' ');

						if (name.Length == 0)
						{
							continue;
						}

						string firstName = name[0];
						string lastName = string.Empty;

						if (name.Length > 1)
						{
							StringBuilder sb = new StringBuilder();

							for (int i = 1; i < name.Length; i++)
							{
								if (sb.Length > 0)
								{
									sb.Append(' ');
								}
								sb.Append(name[i]);
							}
							lastName = sb.ToString();
						}

						var existingActor = actors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
						if (existingActor is not null)
						{
							Movie_Actor newMovie = new Movie_Actor();
							newMovie.Movie = movie;
							newMovie.ActorId = existingActor.Id;
							movie.MovieActors.Add(newMovie);
						}
					}
					db.Movies.Add(movie);
				}
				await db.SaveChangesAsync();
			}
		}

		public async Task GetNewActor(Actor userActor)
		{
			var actorID = await AddNewActor(userActor);
			string actorName = $"{userActor.FirstName} {userActor.LastName}";
			var response = await client.GetAsync($"https://imdb-api.com/en/API/SearchName/k_ttuui8u4/{actorName}");
			string json = await response?.Content?.ReadAsStringAsync() ?? string.Empty;

			if (string.IsNullOrEmpty(json))
			{
				return;
			}

			try
			{
				//search for the actor imdb id
				var actorApiList = JsonConvert.DeserializeObject<ActorList_API>(json);
				var id = actorApiList.results[0].id;

				//search for all actor imdb movies
				var response2 = await client.GetAsync($"https://imdb-api.com/en/API/Name/k_ttuui8u4/{id}");
				string json2 = await response2?.Content?.ReadAsStringAsync() ?? string.Empty;

				if (string.IsNullOrEmpty(json2))
				{
					return;
				}

				var actorMovieApiList = JsonConvert.DeserializeObject<ActorMovieList_API>(json2);

				List<Movie> movieList = new List<Movie>();

				foreach (var castMovie in actorMovieApiList.castMovies)
				{
					if (castMovie.description.Contains('('))
					{
						continue;
					}

					if (castMovie.role != "Actor" && castMovie.role != "Actress")
					{
						continue;
					}

					Movie movie = new Movie();
					movie.Title = castMovie.title;
					movie.Year = 9999;

					if (int.TryParse(castMovie.year, out int year))
					{
						movie.Year = year;
					}

					Movie_Actor newMovieActor = new Movie_Actor();
					newMovieActor.ActorId = actorID;
					newMovieActor.Movie = movie;
					movie.MovieActors.Add(newMovieActor);

					await AddNewMovie(movie);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async Task<int> AddNewActor(Actor userActor)
		{
			using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
			{
				var exists = db.Actors.FirstOrDefault(x => x.FirstName == userActor.FirstName && x.LastName == userActor.LastName);
				if (exists is not null)
				{
					return exists.Id;
				}
				db.Actors.Add(userActor);
				db.SaveChanges();
				return userActor.Id;
			}
		}

		private async Task<int> AddNewMovie(Movie newMovie)
		{
			using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
			{
				var existingMovie = db.Movies.FirstOrDefault(x => x.Title == newMovie.Title && x.Year == newMovie.Year);
				if (existingMovie is not null)
				{
					var movieActorIDs = existingMovie.MovieActors.Select(x => x.ActorId).ToList();
					foreach (var actorID in newMovie.MovieActors.Select(x => x.ActorId))
					{
						if (!movieActorIDs.Contains(actorID))
						{
							Movie_Actor newMovieActor = new Movie_Actor();
							newMovieActor.ActorId = actorID;
							newMovieActor.Movie = existingMovie;
							existingMovie.MovieActors.Add(newMovieActor);
							db.SaveChanges();
						}
					}
					return existingMovie.Id;
				}
				db.Movies.Add(newMovie);
				db.SaveChanges();
				return newMovie.Id;
			}
		}

		// THIS IS THE InsertNewMovie() METHOD WE WERE WORKING ON BUT DIDN'T END UP USING

		//private async Task InsertNewMovie(List<Movie> movieList, Actor userActor)
		//{
		//	using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
		//	{
		//		var movies = await db.Movies.AsNoTracking().ToListAsync();
		//		var id = db.Actors.FirstOrDefault(x => x.FirstName == userActor.FirstName && x.LastName == userActor.LastName).Id;

		//		foreach (var movie in movieList)
		//		{
		//			var existingMovie = movies.FirstOrDefault(x => x.Title == movie.Title && x.Year == movie.Year);
		//			if (existingMovie is not null)
		//			{
		//				//link the actor to the existing movie
		//				Movie_Actor newMovie = new Movie_Actor();
		//				newMovie.MovieId = existingMovie.Id;
		//				newMovie.ActorId = id;
		//				movie.MovieActors.Add(newMovie);

		//				continue;
		//			}
		//			Movie_Actor newMovie2 = new Movie_Actor();
		//			newMovie2.Movie = movie;
		//			newMovie2.ActorId = id;
		//			movie.MovieActors.Add(newMovie2);

		//			db.Movies.Add(movie);
		//		}
		//		await db.SaveChangesAsync();
		//	}
		//}
	}
}