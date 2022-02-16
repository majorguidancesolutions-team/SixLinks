using DataLibrary;
using MyDataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyDataManagerWinForms.MainForm;

namespace MyDataManagerWinForms
{
    public partial class AddMovieForm : Form
    {
        public event PopulateMessageEvent populateMessageVariable;
        private Movie _movie;
        public AddMovieForm()
        {
            InitializeComponent();
        }

        public AddMovieForm(Movie movie)
        {
            InitializeComponent();
            _movie = movie;
            this.txtMovieId.Text = movie.Id.ToString();
            this.txtMovieTitle.Text = movie.Title;
            this.txtMovieYear.Text = movie.Year.ToString();

        }

        private void btnOkMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtMovieId.Text))
            {
                if (string.IsNullOrWhiteSpace(this.txtMovieTitle.Text) && string.IsNullOrWhiteSpace(this.txtMovieYear.Text))
                {
                    MessageBox.Show("Enter an valid movie title.", "Missing Movie Title", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                }

                if (!int.TryParse(this.txtMovieYear.Text, out int newYear))
                {
                    MessageBox.Show("Enter a valid year", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                addNewMovie(this.txtMovieTitle.Text, newYear);
            }
            else
            {
                updateMovie(this.txtMovieId.Text, this.txtMovieTitle.Text, this.txtMovieYear.Text);
            }
            this.Close();
        }

        private void addNewMovie(string title, int year)
        {
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                var newMovie = new Movie();
                newMovie.Title = title;
                newMovie.Year = year;


                var existingMovie = db.Movies.FirstOrDefault(x => x.Title == newMovie.Title
                                                             && x.Year == newMovie.Year);

                if (existingMovie is null)
                {
                    db.Add(newMovie);
                    db.SaveChanges();

                    if (populateMessageVariable is not null)
                    {
                        populateMessageVariable($"{newMovie.Title} ({newMovie.Year}) added");
                    }
                }

                else
                {
                    MessageBox.Show($"{existingMovie.Title} {existingMovie.Year} is already in the database", "Existing Movie",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                }

            }
        }

        private void updateMovie(string movieId, string movieTitle, string movieYear)
        {
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                var existingMovie = db.Movies.FirstOrDefault(x => x.Id == Convert.ToInt32(movieId));
                existingMovie.Title = movieTitle;
                if (!int.TryParse(movieYear, out int newYear))
                {
                    MessageBox.Show("Enter a valid year");
                    return;
                }
                existingMovie.Year = newYear;
                db.SaveChanges();

                if (populateMessageVariable is not null)
                {
                    populateMessageVariable($"{existingMovie.Title} ({existingMovie.Year}) added");
                }
            }
        }

        private void btnCancelMovie_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
