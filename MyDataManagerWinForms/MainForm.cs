using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDataManagerDataOperations;
using MyDataModels;
using System.Diagnostics;

namespace MyDataManagerWinForms
{
    public partial class MainForm : Form
    {
        //public static IConfigurationRoot _configuration;
        //public static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;

        public delegate void PopulateMessageEvent(string message);

        private IList<Movie> Movies = new List<Movie>();
        private IList<Actor> Actors = new List<Actor>();
        private IList<Movie_Actor> MovieActors = new List<Movie_Actor>();

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
            var dataOps = new DataOperations();

            Movies = Task.Run(() => dataOps.GetMovies()).Result;
            cboMovies.DataSource = Movies;
            cboMovies.SelectedIndex = -1;

            Actors = Task.Run (() => dataOps.GetActors()).Result;
            cboActors.DataSource = Actors;
            cboActors.SelectedIndex = -1;
        }

        //static void BuildOptions()
        //{
        //    _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        //    _optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
        //    _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDataManagerData"));
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            //BuildOptions();

            // TODO: Build "progress bar" or MessageBox prompt to notify user that data is being imported.

            // if no data then run the data importer
            var dataOps = new DataOperations();
            try
            {
                Task.Run(async() => await dataOps.InitialDatabaseLoad());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            var dataOps = new DataOperations();

            dgItems.DataSource = Task.Run(() => dataOps.GetActorsFromDB(selectedMovie)).Result;
        }

        private void LoadActorGrid(Actor selectedActor)
        {
            if (selectedActor is null)
            {
                return;
            }

            var dataOps = new DataOperations();

            dgItems.DataSource = Task.Run(() => dataOps.GetMoviesFromDB(selectedActor)).Result;
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

        //TO DO: if an actor is deleted any movies that are only associated with that actor should be deleted
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
                    var dataops = new DataOperations();
                    Task.Run(async() => await dataops.DeleteMovie(selMovie));
                    cboMovies.SelectedIndex = -1;
                    Refresh();
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
                    var dataops = new DataOperations();
                    Task.Run(async () => await dataops.DeleteActor(selActor));
                   
                    cboActors.SelectedIndex = -1;
                    Refresh();
                    MessageBox.Show($"{selActor.FirstName} {selActor.LastName} deleted");
                }
            }
        }
    }
}