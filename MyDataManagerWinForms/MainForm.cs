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

		public delegate void PopulateMessageEvent(string message);

		private IList<Movie> Movies = new List<Movie>();
		private IList<Actor> Actors = new List<Actor>();
		private IList<Movie_Actor> MovieActors = new List<Movie_Actor>();

		private const int PROGRESS_TIME = 30000;

		public MainForm()
		{
			InitializeComponent();
		}

		private void UpdateMessageEvent(string message)
		{
			MessageBox.Show(message);
			Refresh();
		}

		public void Refresh()
		{
			// load categories
			using (var db = new DataDbContext(_optionsBuilder.Options))
			{
				Movies = db.Movies.Include(x => x.MovieActors).OrderBy(x => x.Title).ToList();
				Actors = db.Actors.Include(x => x.ActorMovies).OrderBy(x => x.FirstName).ToList();
				MovieActors = db.Movies_Actors.OrderBy(x => x.Id).ToList();

				cboMovies.DataSource = Movies;
				cboMovies.SelectedIndex = -1;

				cboActors.DataSource = Actors;
				cboActors.SelectedIndex = -1;
			}
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

			// TODO: Build "progress bar" or MessageBox prompt to notify user that data is being imported.
			
			// if no data then run the data importer
			using (var db = new DataDbContext(_optionsBuilder.Options))
			{
				if (!db.Movies.Any() && !db.Actors.Any())
				{
					var di = new DataImporter();
					Task.Run(async () => await di.GetInitialData());
					var progForm = new ProgressBarForm();
					progForm.ShowDialog();
					//Thread.Sleep(PROGRESS_TIME);
					//progForm.Close();
				}
			}

			Refresh();
		}

		private void cboMovies_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboMovies.SelectedIndex == -1)
			{
				return;
			}

			var cboBox = sender as ComboBox;
			var selMovie = cboBox.SelectedItem as Movie;

			LoadMovieGrid(selMovie);

			cboActors.SelectedIndex = -1;
		}

		private void cboActors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboActors.SelectedIndex == -1)
			{
				return;
			}

			var cboBox = sender as ComboBox;
			var selActor = cboBox.SelectedItem as Actor;

			LoadActorGrid(selActor);

			cboMovies.SelectedIndex = -1;
		}

		private void LoadMovieGrid(Movie selectedMovie)
		{
			if (selectedMovie is null)
			{
				return;
			}

			//Debug.WriteLine($"Selected Movie {selectedMovie.Id}| {selectedMovie.Title}");
			//var actorMovies = selectedMovie.MovieActors.Where(x => x.MovieId == selectedMovie.Id).ToList();
			//var theActors = new List<Actor>();

			//foreach (var movie in actorMovies)
			//{
			//    var actor = Actors.SingleOrDefault(x => x.Id == movie.ActorId);
			//    if (actor != null)
			//    {
			//        theActors.Add(actor);
			//    }
			//}

			//dgItems.DataSource = theActors;

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

				if (movieData != null)
				{
					var actors = movieData.Actors;
					dgItems.DataSource = actors;
				}
			}
		}

		private void LoadActorGrid(Actor selectedActor)
		{
			if (selectedActor is null)
			{
				return;
			}

			using (var db = new DataDbContext(_optionsBuilder.Options))
			{
				var selectedActorsFilms = db.Movies
							.Join(db.Movies_Actors, movie => movie.Id, movact => movact.MovieId,
								(movie, movact) => new { Title = movie.Title, Year = movie.Year, Id = movact.ActorId })
							.Join(db.Actors, x => x.Id, actor => actor.Id,
								(x, actor) => new { x.Id, x.Title, x.Year })
							.Where(x => x.Id == selectedActor.Id)
							.Select(n => new { n.Title, n.Year }).ToList();

				dgItems.DataSource = selectedActorsFilms;
			}
		}

		//      private void BtnDataImport_Click(object sender, EventArgs e)
		//      {

		//}

		private void btnAddActor_Click(object sender, EventArgs e)
		{
			var addActor = new AddActorForm();
			addActor.populateMessageVariable += new PopulateMessageEvent(UpdateMessageEvent);
			addActor.ShowDialog();
		}

		private void btnAddMovie_Click(object sender, EventArgs e)
		{
			var addMovie = new AddMovieForm();
			addMovie.populateMessageVariable += new PopulateMessageEvent(UpdateMessageEvent);
			addMovie.ShowDialog();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			if (cboActors.SelectedIndex == -1 && cboMovies.SelectedIndex == -1)
			{
				MessageBox.Show("Choose an actor or movie to update.", "No Selection Made", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			// if a movie is selected
			if (cboMovies.SelectedIndex != -1)
			{
				var selMovie = cboMovies.SelectedItem as Movie;

				var addMovie = new AddMovieForm(selMovie);
				addMovie.populateMessageVariable += new PopulateMessageEvent(UpdateMessageEvent);
				addMovie.ShowDialog();
			}

			// if an actor is selected
			if (cboActors.SelectedIndex != -1)
			{
				var selActor = cboActors.SelectedItem as Actor;

				var addActor = new AddActorForm(selActor);
				addActor.populateMessageVariable += new PopulateMessageEvent(UpdateMessageEvent);
				addActor.ShowDialog();
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (cboActors.SelectedIndex == -1 && cboMovies.SelectedIndex == -1)
			{
				MessageBox.Show("Choose an actor or movie to delete.", "No Selection Made", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}
			// if a movie is selected
			if (cboMovies.SelectedIndex != -1)
			{
				var selMovie = cboMovies.SelectedItem as Movie;

				DialogResult userSelection = MessageBox.Show($"Do you confirm delete of {selMovie}?", "Confirm Delete",
																	MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (userSelection == DialogResult.OK)
				{
					var deleteID = selMovie.Id;
					using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
					{
						var movieToRemove = db.Movies.SingleOrDefault(x => x.Id == deleteID);
						if (movieToRemove != null)
						{
							db.Movies.Remove(movieToRemove);
							db.SaveChanges();
							cboMovies.SelectedIndex = -1;
							Refresh();
						}
					}
					MessageBox.Show($"{selMovie.Title} ({selMovie.Year}) deleted");
				}
			}

			// if an actor is selected
			if (cboActors.SelectedIndex != -1)
			{
				var selActor = cboActors.SelectedItem as Actor;

				DialogResult userSelection = MessageBox.Show($"Do you confirm delete of {selActor}?", "Confirm Delete",
																	MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (userSelection == DialogResult.OK)
				{
					var deleteID = selActor.Id;
					using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
					{
						var actorToRemove = db.Actors.SingleOrDefault(x => x.Id == deleteID);
						if (actorToRemove != null)
						{
							db.Actors.Remove(actorToRemove);
							db.SaveChanges();
							cboActors.SelectedIndex = -1;
							Refresh();
						}
					}
					MessageBox.Show($"{selActor.FirstName} {selActor.LastName} deleted");
				}
			}
		}
	}
}