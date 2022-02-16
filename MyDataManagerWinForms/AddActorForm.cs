using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

namespace MyDataManagerWinForms
{

    public partial class AddActorForm : Form
    {
        public static DbContextOptionsBuilder<DataDbContext> _optionsBuilder;
        private Actor _actor;
        public AddActorForm()
        {
            InitializeComponent();
        }
        public AddActorForm(Actor actor)
        {
            InitializeComponent();
            _actor = actor;
            this.txtActorId.Text = actor.Id.ToString();
            this.txtFirstName.Text = actor.FirstName;
            this.txtLastName.Text = actor.LastName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtActorId.Text))
            {
                if (string.IsNullOrWhiteSpace(this.txtFirstName.Text))
                {
                    MessageBox.Show("Enter an actor's first name.", "Missing Actor Name", MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    return;
                }

                else if (string.IsNullOrWhiteSpace(this.txtLastName.Text))
                {
                    DialogResult userSelection = MessageBox.Show("Does this actor have a last name?", "Missing Actor Name",
                                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (userSelection == DialogResult.Yes)
                    {
                        MessageBox.Show("Enter the actor's last name.", "Missing Actor Name", MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        //add the actor
                        AddActor(this.txtFirstName.Text, string.Empty);
                    }
                }
                else
                {
                    AddActor(this.txtFirstName.Text, this.txtLastName.Text);
                }
            }
            else
            {
                UpdateActor(this.txtActorId.Text, this.txtFirstName.Text, this.txtLastName.Text);
            }
            this.Close();
        }

        private void AddActor(string firstName, string lastName)
        {
            //check that the input is not in database
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                var userActor = new Actor();
                userActor.FirstName = firstName;
                userActor.LastName = lastName;

                var existingActor = db.Actors.FirstOrDefault(x => x.FirstName == userActor.FirstName
                                                             && x.LastName == userActor.LastName);

                if (existingActor is null)
                {
                    db.Add(userActor);
                    db.SaveChanges();
                }

                else
                {
                    MessageBox.Show($"{userActor.FirstName} {userActor.LastName} is already in the database.", "Existing Actor",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            //if not then we need to search api

        }

        private void UpdateActor(string actorId, string firstName, string lastName)
        {
            
            using (var db = new DataDbContext(MainForm._optionsBuilder.Options))
            {
                var existingActor = db.Actors.FirstOrDefault(x => x.Id == Convert.ToInt32(actorId));
                existingActor.FirstName = firstName;
                existingActor.LastName = lastName;
                db.SaveChanges();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
