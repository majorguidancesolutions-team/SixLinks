using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataModels;
using System.Diagnostics;

namespace MyDataManagerWinForms
{
	public partial class MainForm : Form
	{
		public static IConfigurationRoot _configuration;
		public static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;

		private IList<Category> Categories = new List<Category>();
		private IList<Item> Items = new List<Item>();

		private IList<Movie> Movies = new List<Movie>();
		private IList<Actor> Actors = new List<Actor>();
		private IList<Movie_Actor> MovieActors = new List<Movie_Actor>();

		public MainForm()
		{
			InitializeComponent();
		}

		static void BuildOptions()
		{
			_configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
			_optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
			_optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDataManagerData"));
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			BuildOptions();

			//load categories
			using (var db = new DataDbContext(_optionsBuilder.Options))
			{
				//Categories = db.Categories.OrderBy(x => x.Name).ToList();
				//Items = db.Items.ToList();
				//cboCategories.DataSource = Categories;

				Movies = db.Movies.Include(x => x.MovieActors).OrderBy(x => x.Title).ToList();
				Actors = db.Actors.Include(x => x.ActorMovies).OrderBy(x => x.FirstName).ToList();
				MovieActors = db.Movies_Actors.OrderBy(x => x.Id).ToList();

				cboMovies.DataSource = Movies;
				cboActors.DataSource = Actors;


			}
		}
		//private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    //var cboBox = sender as ComboBox;
		//    //var selMovie = cboBox.SelectedItem as Movie;

		//    //LoadMovieGrid(selMovie);
		//}
		private void cboMovies_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cboBox = sender as ComboBox;
			var selMovie = cboBox.SelectedItem as Movie;

			LoadMovieGrid(selMovie);
		}

		private void cboActors_SelectedIndexChanged(object sender, EventArgs e)
		{
			var cboBox = sender as ComboBox;
			var selActor = cboBox.SelectedItem as Actor;

			LoadActorGrid2(selActor);
		}

		/*private void LoadGrid(Movie selectedItem)
        {
            Debug.WriteLine($"Selected Item {selectedItem.Id}| {selectedItem.Name}");
            var curData = Items.Where(x => x.CategoryId == selectedItem.Id).OrderBy(x => x.Name).ToList();
            dgItems.DataSource = curData;
        }*/

		private void LoadMovieGrid(Movie selectedMovie)
		{
			Debug.WriteLine($"Selected Movie {selectedMovie.Id}| {selectedMovie.Title}");
			var actorMovies = selectedMovie.MovieActors.Where(x => x.MovieId == selectedMovie.Id).ToList();
			var theActors = new List<Actor>();
			foreach (var movie in actorMovies)
			{
				var actor = Actors.SingleOrDefault(x => x.Id == movie.ActorId);
				if (actor != null)
				{
					theActors.Add(actor);
				}
			}

			dgItems.DataSource = theActors;

			using (var db = new DataDbContext(_optionsBuilder.Options))
			{
				var movieData = db.Movies
								.Include(x => x.MovieActors)
								.ThenInclude(y => y.Actor)
								.Select(x => new { 
									Id = x.Id,
									Title = x.Title,
									Actors = 
										x.MovieActors.Select(y => y.Actor)
								})
								.FirstOrDefault(x => x.Id == selectedMovie.Id);


				if (movieData != null)
                {
					var actors = movieData.Actors;
					dgItems.DataSource = actors;
				}
			}
		}

		private void LoadActorGrid(Actor selectedActor)
		{
			Debug.WriteLine($"Selected Actor {selectedActor.Id}| {selectedActor.FirstName} {selectedActor.LastName}");
			var actorMovies = selectedActor.ActorMovies.Where(x => x.ActorId == selectedActor.Id).ToList();
			var theMovies = new List<Movie>();
			foreach (var actors in actorMovies)
			{
				var movie = Movies.SingleOrDefault(x => x.Id == actors.MovieId);
				if (movie != null)
				{
					theMovies.Add(movie);
				}
			}

			dgItems.DataSource = theMovies;
		}

		private void LoadActorGrid2(Actor selectedActor)
		{
			using (var db = new DataDbContext(_optionsBuilder.Options))
			{

                //var selectedFilteredItems = await db.Items.AsNoTracking().Select(item => new
                //{
                //    Id = item.Id,
                //    Name = item.Name,
                //    Players = item.Players.Where(player => player.Name.Contains("ar"))
                //                          .Select(player => new Player
                //                          {
                //                              Id = player.Id,
                //                              Name = player.Name
                //                          }).ToList()
                //}).ToListAsync();

				var selectedActorsFilms = db.Movies
							.Join(db.Movies_Actors, movie => movie.Id, movact => movact.MovieId,
								(movie, movact) => new {Title = movie.Title, Year = movie.Year, Id = movact.ActorId})
							.Join(db.Actors, x => x.Id, actor => actor.Id,
								(x, actor) => new {x.Id, x.Title, x.Year})
							.Where(x => x.Id == selectedActor.Id)
							.Select(n => new {n.Title, n.Year}).ToList();

				dgItems.DataSource = selectedActorsFilms;

			}
		}

		private void BtnDataImport_Click(object sender, EventArgs e)
		{
			var di = new DataImporter();
			Task.Run(async () => await di.ImportData());
		}
	}
}