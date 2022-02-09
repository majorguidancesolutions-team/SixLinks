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

namespace MyDataManagerWinForms
{
    public class DataImporter
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task ImportData()
        {
            await GetData();
            
        }

        private async Task InsertMovies(List<Movie> movieList)
        {
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                if (db.Movies.Any())
                {
                    return;
                }

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

        private async Task InsertActors(List<Actor> actorsList)
        {
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                if(db.Actors.Count() == 0)
                {
                    await db.Actors.AddRangeAsync(actorsList);
                    await db.SaveChangesAsync();
                }
                
            }
        }
        private async Task GetData()
        {
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
                        if(member.Contains("(dir.)"))
                        {
                            continue;
                        }
                        Debug.WriteLine(member);
                        var name = member.Trim().Split(' ');

                        if(name.Length == 0)
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

                //call to insert actors
                await InsertActors(ourActors);

                await InsertMovies(ourMovies);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            
        }
    }
}
